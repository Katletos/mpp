using System.Diagnostics;

namespace TaskQueue;

public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
{
    // Indicates whether the current thread is processing work items.
    [ThreadStatic] private static bool _currentThreadIsProcessingItems;

    private readonly LinkedList<Task> _tasks = new(); // protected by lock(_tasks)

    private readonly int _maxDegreeOfParallelism;

    private int _delegatesQueuedOrRunning;

    public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));

        _maxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    protected sealed override void QueueTask(Task task)
    {
        lock (_tasks)
        {
            _tasks.AddLast(task);
            if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
            {
                ++_delegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }
    }

    // Inform the ThreadPool that there's work to be executed for this scheduler.
    private void NotifyThreadPoolOfPendingWork()
    {
        ThreadPool.UnsafeQueueUserWorkItem(_ =>
        {
            // Note that the current thread is now processing work items.
            // This is necessary to enable inlining of tasks into this thread.
            _currentThreadIsProcessingItems = true;
            try
            {
                while (true)
                {
                    Task item;
                    lock (_tasks)
                    {
                        // When there are no more items to be processed,
                        // note that we're done processing, and get out.
                        if (_tasks.Count == 0)
                        {
                            --_delegatesQueuedOrRunning;
                            break;
                        }

                        Debug.Assert(_tasks.First != null, "_tasks.First != null");
                        item = _tasks.First.Value;
                        _tasks.RemoveFirst();
                    }

                    TryExecuteTask(item);
                }
            }
            finally
            {
                _currentThreadIsProcessingItems = false;
            }
        }, null);
    }

    // Attempts to execute the specified task on the current thread.
    protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        // If this thread isn't already processing a task, we don't support inlining
        if (!_currentThreadIsProcessingItems) return false;

        // If the task was previously queued, remove it from the queue
        if (taskWasPreviouslyQueued)
        {
            if (TryDequeue(task))
            {
                return TryExecuteTask(task);
            }

            return false;
        }

        return TryExecuteTask(task);
    }

    protected sealed override IEnumerable<Task> GetScheduledTasks()
    {
        bool lockTaken = false;
        try
        {
            Monitor.TryEnter(_tasks, ref lockTaken);
            if (lockTaken) return _tasks;
            else throw new NotSupportedException();
        }
        finally
        {
            if (lockTaken) Monitor.Exit(_tasks);
        }
    }
}
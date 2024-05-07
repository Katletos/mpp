using FluentAssertions;
using TaskQueue;

namespace TaskQueueTest;

public class TestTaskQueue
{
    [Fact]
    public void Test1()
    {
        var lcts = new LimitedConcurrencyLevelTaskScheduler(2);
        var factory = new TaskFactory(lcts);
        var tasks = new List<Task<int>>();
        var cts = new CancellationTokenSource();
        List<int> result = [5, 5, 5, 5, 5];
        
        for (int i = 0; i < 5; i++) {
           var t = factory.StartNew(() => 5, cts.Token);
           tasks.Add(t);
        }
        Task.WaitAll(tasks.ToArray());
        cts.Dispose();

        tasks.Select(x => x.Result).Should().Equal(result);
    }
}
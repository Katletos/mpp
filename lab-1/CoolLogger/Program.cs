var cl = new CoolLogger.CoolLogger(
    "./test.txt",
    2,
    TimeSpan.FromSeconds(5));

for (int i = 0; i < 13; i++)
{
    cl.AddLog($"iteration {i}");
}

Console.ReadKey();

var callbacks = new WaitCallback[2];
callbacks[0] = x => Console.WriteLine("write 1");
callbacks[1] = x => Console.WriteLine("write 2");

ParallelWaitAll(callbacks);

Console.WriteLine("Completed");

static void ParallelWaitAll(WaitCallback[] actions)
{
    foreach (var action in actions)
    {
        ThreadPool.QueueUserWorkItem(action, null);
    }

    while (ThreadPool.PendingWorkItemCount is not 0)
    {
        Thread.Yield();
    }
}
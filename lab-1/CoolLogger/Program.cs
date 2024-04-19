var cl = new CoolLogger.CoolLogger(
    "./test.txt",
    2,
    TimeSpan.FromSeconds(5));

for (int i = 0; i < 13; i++)
{
    cl.AddLog($"iteration {i}");
}

Console.ReadKey();

ParallelWhaitAll(null);

Console.WriteLine("Completed");

static void ParallelWhaitAll(WaitCallback[] actions)
{
    foreach (var action in actions)
    {
        ThreadPool.QueueUserWorkItem(action , null);
    }

    while (ThreadPool.PendingWorkItemCount is not 0)
    {
        Thread.Yield();
    }
}
using Mutex;

var mutex = new Mutex.Mutex();
int sharedResource = 0;

Console.WriteLine("Enter the number of threads:");
int numThreads = int.Parse(Console.ReadLine() ?? string.Empty);

for (int i = 0; i < numThreads; i++)
{
    ThreadPool.QueueUserWorkItem(IncrementSharedResource);
}

while (ThreadPool.PendingWorkItemCount != 0)
{
    Thread.Yield();
}

Console.WriteLine("Final value of sharedResource: " + sharedResource);

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

void IncrementSharedResource(object? state)
{
    mutex.Lock();
    sharedResource++;
    mutex.Unlock();
}

/////////////////////////////////////

var filee = File.Open("./test.txt", FileMode.Create);

using var poseHandle = new PoseHandle(filee.SafeFileHandle);
Console.WriteLine("Handle value: " + poseHandle.GetHandle().IsClosed);
poseHandle.ReleaseHandle();
Console.WriteLine("Handle value: " + poseHandle.GetHandle().IsClosed);
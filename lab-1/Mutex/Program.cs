using Mutex;

var mutex = new Mutex.Mutex();
int sharedResource = 0;

Console.WriteLine("Enter the number of threads:");
int numThreads = int.Parse(Console.ReadLine() ?? string.Empty);

for (int i = 0; i < numThreads; i++)
{
    ThreadPool.QueueUserWorkItem(IncrementSharedResource);
}

Thread.Sleep(1000);
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

var file = File.Open("./test.txt", FileMode.Create);

using var poseHandle = new OsHandle(file.SafeFileHandle);
Console.WriteLine("Handle value: " + poseHandle.GetHandle().IsClosed);
poseHandle.ReleaseHandle();
Console.WriteLine("Handle value: " + poseHandle.GetHandle().IsClosed);
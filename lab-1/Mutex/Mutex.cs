using static System.Threading.Interlocked;

namespace Mutex;

public class Mutex
{
    private Thread? _thread;
    
    public void Lock()
    {
        var t = Thread.CurrentThread;
        while (CompareExchange(ref _thread, t, null) is not null)
        {
            Thread.Yield();
        }
        Thread.MemoryBarrier();
    }
    
    public void Unlock()
    {
        var t = Thread.CurrentThread;
        if (CompareExchange(ref _thread, null, t) != t)
        {
            throw new SynchronizationLockException();
        }
        Thread.MemoryBarrier();
    }
}
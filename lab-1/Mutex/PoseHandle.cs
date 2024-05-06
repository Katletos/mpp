namespace Mutex;

public class PoseHandle : IDisposable
{
    private IntPtr _handle;
    private bool _disposed = false;

    public PoseHandle(IntPtr initialHandle)
    {
        _handle = initialHandle;
    }

    public IntPtr Handle
    {
        get
        {
            if (!_disposed) return _handle;

            throw new ObjectDisposedException(ToString());
        }

        set { _handle = value; }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing){}
        CloseHandle(_handle);
        _handle = IntPtr.Zero;

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PoseHandle()
    {
        Dispose(false);
    }

    public void ReleaseHandle()
    {
        Dispose();
    }

    private extern static bool CloseHandle(IntPtr hObject);
}
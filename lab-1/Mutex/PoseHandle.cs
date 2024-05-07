using System.Reflection.Metadata;
using Microsoft.Win32.SafeHandles;

namespace Mutex;

public sealed class PoseHandle : IDisposable
{
    private bool _disposed;
    private readonly SafeFileHandle _handle;

    public PoseHandle(SafeFileHandle handle)
    {
        _handle = handle;
    }

    public SafeFileHandle GetHandle()
    {
        if (!_disposed) return _handle;
        throw new ObjectDisposedException(ToString());
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
        }

        _handle.Dispose();
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
}
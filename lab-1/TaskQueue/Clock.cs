namespace TaskQueue;

public class Clock
{
    private event EventHandler OnTimer;
    private readonly TimeSpan _duration;
    private Timer _timer;

    public Clock(TimeSpan duration)
    {
        _duration = duration;
    }

    public void Start()
    {
    }

    public void Invoke()
    {
        
    }
}
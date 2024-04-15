namespace TaskQueue;

public class Clock
{
    public event EventHandler? OnTimer;
    private readonly TimeSpan _duration;
    
    public Clock(TimeSpan duration)
    {
        _duration = duration;
    }

    public void Start()
    {
        new Thread(StartClock).Start();
    }

    protected virtual void StartClock()
    {   
        Thread.Sleep(_duration);
        if (OnTimer is not null)
        {
            OnTimer.Invoke(this, EventArgs.Empty);
        }
    }
}
using System.Timers;

namespace Jpc.Shared.Utilities;

public sealed class ClockGenerator : IDisposable
{
    private readonly System.Timers.Timer _timer;
    private readonly Func<Task>? _asyncHandler;

    public ClockGenerator(TimeSpan interval, bool repeat = true, Func<Task>? asyncHandler = null)
    {
        _asyncHandler = asyncHandler;

        _timer = new System.Timers.Timer();
        _timer.Elapsed += new ElapsedEventHandler(ClockHandler);
        _timer.Interval = interval.TotalMilliseconds;
        _timer.Enabled = true;
        _timer.AutoReset = repeat;
    }

    public event EventHandler? OnTrigger;

    public void Dispose()
    {
        _timer.Stop();
        _timer.Elapsed -= new ElapsedEventHandler(ClockHandler);
        _timer.Dispose();
    }

    private async void ClockHandler(object? sender, EventArgs a)
    {
        OnTrigger?.Invoke(this, a);

        if (_asyncHandler is not null)
            await _asyncHandler.Invoke();
    }
}

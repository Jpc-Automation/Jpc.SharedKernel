using System.Collections.Concurrent;

namespace Jpc.Shared.Utilities;

public class TriggeredBuffer<T> : BufferT<T>
{
    private readonly ClockGenerator _clockGenerator;

    public TriggeredBuffer(Action<T>? itemHandler = null,
                    Func<T, Task>? asyncItemHandler = null,
                    Action? finalHandler = null,
                    Func<Task>? asyncFinalHandler = null,
                    int intervalSeconds = 10) : base(itemHandler, asyncItemHandler, finalHandler, asyncFinalHandler)
    {
        _clockGenerator = new ClockGenerator(TimeSpan.FromSeconds(intervalSeconds), true, Deque);
    }
}

public class BufferT<T>
{
    private readonly ConcurrentQueue<T> _queue = new();

    private readonly Action<T>? _itemHandler;
    private readonly Func<T, Task>? _asyncItemHandler;

    private readonly Action? _finalHandler;
    private readonly Func<Task>? _asyncFinalHandler;

    protected bool isRunning = false;

    public BufferT(Action<T>? itemHandler = null,
                    Func<T, Task>? asyncItemHandler = null,
                    Action? finalHandler = null,
                    Func<Task>? asyncFinalHandler = null)
    {
        _itemHandler = itemHandler;
        _asyncItemHandler = asyncItemHandler;
        _finalHandler = finalHandler;
        _asyncFinalHandler = asyncFinalHandler;
    }

    public void Add(T item)
        => _queue.Enqueue(item);

    public async Task Deque()
    {
        if (isRunning)
            return;

        isRunning = true;

        try
        {
            await HandleQueue();
        }
        finally
        {
            isRunning = false;
        }

        return;
    }

    private async Task HandleQueue()
    {
        while (_queue.TryDequeue(out var localValue))
        {
            _itemHandler?.Invoke(localValue);

            if (_asyncItemHandler is not null)
                await _asyncItemHandler(localValue);
        }

        _finalHandler?.Invoke();

        if (_asyncFinalHandler is not null)
            await _asyncFinalHandler.Invoke();

        return;
    }
}

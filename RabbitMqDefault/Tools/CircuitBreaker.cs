public class CircuitBreaker
{
    private int failureCount = 0;
    private readonly int maxFailures;
    private readonly TimeSpan timeout;
    private CircuitBreakerState state = CircuitBreakerState.Closed;
    private DateTime lastFailureTime;

    public CircuitBreaker(int maxFailures, TimeSpan timeout)
    {
        this.maxFailures = maxFailures;
        this.timeout = timeout;
    }

    public bool IsOpen => state == CircuitBreakerState.Open;

    public void Execute(Action action, Action onFallback = null)
    {
        if (state == CircuitBreakerState.Open)
        {
            if (DateTime.Now - lastFailureTime > timeout)
            {
                state = CircuitBreakerState.HalfOpen;
            }
            else
            {
                onFallback?.Invoke();
                return;
            }
        }
        try
        {
            action();
            Reset();
        }
        catch (Exception ex)
        {
            HandleFailure(ex, onFallback);
        }
    }

    private void Reset()
    {
        state = CircuitBreakerState.Closed;
        failureCount = 0;
    }

    private void HandleFailure(Exception ex, Action onFallback)
    {
        Console.WriteLine($"Error: {ex.Message}");
        failureCount++;

        if (failureCount >= maxFailures)
        {
            state = CircuitBreakerState.Open;
            lastFailureTime = DateTime.Now;
        }

        onFallback?.Invoke();
    }

    private enum CircuitBreakerState
    {
        Closed,
        HalfOpen,
        Open
    }
}

namespace WorkflowExecutionDemo.Shared;

/// <summary>
/// Provides controlled random delay simulation for workflow execution.
/// </summary>
public static class DelayHelper
{
    private static readonly Random _random = Random.Shared;

    /// <summary>
    /// Asynchronously delays execution for a random interval.
    /// </summary>
    public static Task DelayAsync()
    {
        int delay = _random.Next(Constants.MinDelay, Constants.MaxDelay);
        return Task.Delay(delay);
    }
}

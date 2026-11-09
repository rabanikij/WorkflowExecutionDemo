namespace WorkflowExecutionDemo.Shared;

/// <summary>
/// Handles timestamp formatting for logging.
/// </summary>
public static class TimeStampHelper
{
    /// <summary>
    /// Gets current timestamp in standardized format.
    /// </summary>
    public static string Now()
    {
        return DateTime.Now.ToString(Constants.TimestampFormat);
    }
}

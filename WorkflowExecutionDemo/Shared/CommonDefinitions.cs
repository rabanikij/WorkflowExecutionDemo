namespace WorkflowExecutionDemo.Shared;

/// <summary>
/// Execution priority assigned to a workflow service.
/// </summary>
public enum ServicePriority
{
    High = 1,
    Normal = 2,
    Low = 3
}

/// <summary>
/// Current execution state of a workflow service.
/// </summary>
public enum WorkflowStatus
{
    Created,
    Initialized,
    Waiting,
    Started,
    Processing,
    Completed,
    Failed,
    Resumed
}

/// <summary>
/// Central location for application-wide constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Minimum simulated processing delay (milliseconds).
    /// </summary>
    public const int MinDelay = 500;

    /// <summary>
    /// Maximum simulated processing delay (milliseconds).
    /// </summary>
    public const int MaxDelay = 2500;

    /// <summary>
    /// Too much delayed / wait time simulated processing delay (milliseconds).
    /// </summary>
    public const int TOODELAYED = 10010;

    /// <summary>
    /// Timestamp format used throughout the application.
    /// </summary>
    public const string TimestampFormat = "HH:mm:ss.fff";

    /// <summary>
    /// Workflow execution Duration message.
    /// </summary>
    public const string WorkflowDurationMessage =
        "Workflow execution Duration: ";

    /// <summary>
    /// Workflow started message.
    /// </summary>
    public const string WorkflowStartedMessage =
        "Workflow execution started ...";

    /// <summary>
    /// Standard workflow completion message.
    /// </summary>
    public const string WorkflowCompletedMessage =
        "Workflow execution completed successfully.";

    /// <summary>
    /// Standard workflow failure message.
    /// </summary>
    public const string WorkflowFailedMessage =
        "Workflow execution terminated due to errors.";

    /// <summary>
    /// Simulated workflow failure message.
    /// Too much delayed / wait time unable to process further.
    /// </summary>
    public const string WorkflowDeadLockedMessage =
        "Workflow execution halted due to too much delayed / wait time unable to process further.";
}

using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Models;


/// <summary>
/// Event arguments used when a workflow service reports a status change.
/// </summary>
public sealed class StatusChangedEventArgs : EventArgs
{
    public string ServiceName { get; }
    public ServicePriority Priority { get; }
    public WorkflowStatus Status { get; }
    public string Message { get; }
    public string Timestamp { get; }

    public StatusChangedEventArgs(
        string serviceName,
        ServicePriority priority,
        WorkflowStatus status,
        string message)
    {
        ServiceName = serviceName;
        Priority = priority;
        Status = status;
        Message = message;
        Timestamp = TimeStampHelper.Now();
    }

    /// <summary>
    /// Builds a formatted log line for UI display.
    /// </summary>
    public string ToLogLine()
    {
        return SharedMethods.BuildLogLine(
            Timestamp,
            Priority,
            ServiceName,
            Message);
    }
}

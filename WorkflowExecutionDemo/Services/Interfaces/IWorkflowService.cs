namespace WorkflowExecutionDemo.Services.Interfaces;

using WorkflowExecutionDemo.Shared;

/// <summary>
/// Contract for all workflow services.
/// </summary>
public interface IWorkflowService
{
    string Name { get; }
    ServicePriority Priority { get; }
    WorkflowStatus Status { get; }

    /// <summary>
    /// Executes the service workflow asynchronously.
    /// </summary>
    Task ExecuteAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Raised whenever service status changes.
    /// </summary>
    event EventHandler<Models.StatusChangedEventArgs>? StatusChanged;
}

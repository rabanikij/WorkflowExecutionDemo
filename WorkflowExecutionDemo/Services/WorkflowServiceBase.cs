using System.Windows.Forms;
using WorkflowExecutionDemo.Models;
using WorkflowExecutionDemo.Services.Interfaces;
using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Base class for all workflow services.
/// Implements lifecycle, dependency handling, logging, and execution template.
/// </summary>
public abstract class WorkflowServiceBase : IWorkflowService
{
    private readonly List<WorkflowServiceBase> _dependencies = new();

    protected WorkflowServiceBase()
    {
        SetStatus(WorkflowStatus.Created);
    }

    public abstract string Name { get; }
    public abstract ServicePriority Priority { get; }

    public WorkflowStatus Status { get; private set; }

    public event EventHandler<StatusChangedEventArgs>? StatusChanged;

    /// <summary>
    /// Registers a dependency service.
    /// </summary>
    public void AddDependency(WorkflowServiceBase service)
    {
        _dependencies.Add(service);
    }

    /// <summary>
    /// Executes the full workflow pipeline.
    /// </summary>
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            //Status = WorkflowStatus.Initialized;
            //RaiseStatus(Status.ToString());
            //RaiseStatus("Initialized");
            SetStatus(WorkflowStatus.Initialized);

            await WaitForDependenciesAsync(cancellationToken);

            SetStatus(WorkflowStatus.Processing);

            await ExecuteCoreAsync(cancellationToken);

            SetStatus(WorkflowStatus.Completed);
        }
        catch (Exception ex)
        {
            //Status = WorkflowStatus.Failed;
            //RaiseStatus($"Failed: {ex.Message}");
            SetStatus(WorkflowStatus.Failed, ex.Message);
        }
    }

    /// <summary>
    /// Core business logic implemented by derived services.
    /// </summary>
    protected abstract Task ExecuteCoreAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Waits for all dependencies to complete.
    /// </summary>
    private async Task WaitForDependenciesAsync(CancellationToken cancellationToken)
    {
        if (_dependencies.Count == 0)
        {
            SetStatus(WorkflowStatus.Started);

            return;
        }

        SetStatus(WorkflowStatus.Waiting);

        while (_dependencies.Any(d => d.Status != WorkflowStatus.Completed))
        {
            var dependencyWaitTime = 200 + DateTime.Now.Minute * 2 + DateTime.Now.Second;
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(dependencyWaitTime, cancellationToken); // Dependency Waiting time - simulated by delay
        }

        //RaiseStatus("Dependencies satisfied");
        SetStatus(WorkflowStatus.Resumed);
    }

    /// <summary>
    /// Executes a simulated processing stage with random delay.
    /// </summary>
    protected async Task ExecuteStageAsync(string message, int processDuration, CancellationToken cancellationToken)
    {
        SharedMethods.CheckProcessState(processDuration, message);

        SetStatus(WorkflowStatus.Processing, message);

        await Task.Delay(processDuration, cancellationToken); // Processing Duration - simulated by delay
    }

    /// <summary>
    /// Raises status event safely.
    /// </summary>
    protected void RaiseStatus(string message)
    {
        StatusChanged?.Invoke(
            this,
            new StatusChangedEventArgs(Name, Priority, Status, message));
    }

    protected void SetStatus(WorkflowStatus status, string? message = null)
    {
        //Status step
        //Flow 1 Created -> Initialized -> Processing [No dependency] -> Completed
        //                                 
        //Flow 2 Created -> Initialized -> [When dependency] - Waiting -> [When dependency clearance] - Resumed -> Processing -> Completed
        //
        //Flow 3 Created -> Initialized -> Processing [No dependency] -> [When Failed] - Failed
        //
        switch (status)
        {
            case WorkflowStatus.Created:
                message = "Service created";
                break;
            case WorkflowStatus.Initialized:
                message = Status.ToString();
                break;
            case WorkflowStatus.Waiting:
                message = "Waiting for dependency clearance";
                break;
            case WorkflowStatus.Started:
                message = "No dependencies - ready to start";
                break;
            case WorkflowStatus.Processing:
                break;
            case WorkflowStatus.Completed:
                message = Status.ToString();
                break;
            case WorkflowStatus.Failed:
                message = $"Failed: {message}";
                break;
            case WorkflowStatus.Resumed:
                message = "Dependencies satisfied";
                break;
            default:
                break;
        }

        RaiseStatus(message ?? Status.ToString());
        Status = status;
    }

}

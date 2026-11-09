using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Handles raw material inventory checks and updates.
/// </summary>
public sealed class InventoryService : WorkflowServiceBase
{
    const string STAGE1 = "Checking inventory levels";
    const string STAGE2 = "Reserving raw materials";
    const string STAGE3 = "Updating inventory records";

    public override string Name => nameof(InventoryService);
    public override ServicePriority Priority => ServicePriority.High;

    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var process1Delay = SharedMethods.ProcessingDuration();
        var process2Delay = SharedMethods.ProcessingDuration();
        var process3Delay = SharedMethods.ProcessingDuration();

        await ExecuteStageAsync(STAGE1, process1Delay, cancellationToken);
        await ExecuteStageAsync(STAGE2, process2Delay, cancellationToken);
        await ExecuteStageAsync(STAGE3, process3Delay, cancellationToken);
    }
}

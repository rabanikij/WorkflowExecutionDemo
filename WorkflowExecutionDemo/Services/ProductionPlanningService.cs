using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Plans production based on available inventory and demand.
/// Depends on InventoryService.
/// </summary>
public sealed class ProductionPlanningService : WorkflowServiceBase
{
    const string STAGE1 = "Loading production orders";
    const string STAGE2 = "Analyzing inventory availability";
    const string STAGE3 = "Creating production schedule";

    public override string Name => nameof(ProductionPlanningService);
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

using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Executes actual manufacturing operations.
/// Depends on ProductionPlanningService.
/// </summary>
public sealed class ProductionService : WorkflowServiceBase
{
    const string STAGE1 = "Preparing machines for production";
    const string STAGE2 = "Manufacturing products";
    const string STAGE3 = "Updating production records";

    public override string Name => nameof(ProductionService);
    public override ServicePriority Priority => ServicePriority.Normal;

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

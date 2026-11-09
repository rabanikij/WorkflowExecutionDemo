using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Performs quality inspection on manufactured products.
/// Depends on ProductionService.
/// </summary>
public sealed class QualityControlService : WorkflowServiceBase
{
    const string STAGE1 = "Inspecting products";
    const string STAGE2 = "Running quality tests";
    const string STAGE3 = "Approving production batch";
    const string STAGE4 = "Rejected production batch isolation";

    public override string Name => nameof(QualityControlService);
    public override ServicePriority Priority => ServicePriority.Normal;

    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var process1Delay = SharedMethods.ProcessingDuration();
        var process2Delay = SharedMethods.ProcessingDuration();
        var process3Delay = SharedMethods.ProcessingDuration();
        var process4Delay = SharedMethods.ProcessingDuration();

        await ExecuteStageAsync(STAGE1, process1Delay, cancellationToken);
        await ExecuteStageAsync(STAGE2, process2Delay, cancellationToken);
        await ExecuteStageAsync(STAGE3, process3Delay, cancellationToken);
        await ExecuteStageAsync(STAGE4, process4Delay, cancellationToken);
    }
}

using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Handles product packaging after quality approval.
/// Depends on QualityControlService.
/// </summary>
public sealed class PackagingService : WorkflowServiceBase
{
    const string STAGE1 = "Preparing packaging materials";
    const string STAGE2 = "Packing products, Labeling and Sealing packages";

    public override string Name => nameof(PackagingService);
    public override ServicePriority Priority => ServicePriority.Normal;

    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var process1Delay = SharedMethods.ProcessingDuration();
        var process2Delay = SharedMethods.ProcessingDuration();

        await ExecuteStageAsync(STAGE1, process1Delay, cancellationToken);
        await ExecuteStageAsync(STAGE2, process2Delay, cancellationToken);
    }
}

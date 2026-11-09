using System;
using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Manages storage of packaged goods in warehouse.
/// Depends on PackagingService.
/// </summary>
public sealed class WarehouseService : WorkflowServiceBase
{
    const string STAGE1 = "Receiving packaged goods";
    const string STAGE2 = "Allocating warehouse slots";
    const string STAGE3 = "Updating inventory system";

    public override string Name => nameof(WarehouseService);
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

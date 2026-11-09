using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Handles dispatching goods to customers.
/// Depends on WarehouseService.
/// </summary>
public sealed class ShippingService : WorkflowServiceBase
{
    const string STAGE1 = "Preparing shipment";
    const string STAGE2 = "Loading delivery vehicles";
    const string STAGE3 = "Dispatching goods";

    public override string Name => nameof(ShippingService);
    public override ServicePriority Priority => ServicePriority.Low;

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

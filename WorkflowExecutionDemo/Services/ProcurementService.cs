using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Handles supplier validation and purchase order creation.
/// </summary>
public sealed class ProcurementService : WorkflowServiceBase
{
    const string STAGE1 = "Validating suppliers";
    const string STAGE2 = "Creating purchase orders";
    const string STAGE3 = "Confirming procurement";

    public override string Name => nameof(ProcurementService);
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

using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Monitors machine health, utilization, and status.
/// </summary>
public sealed class MachineMonitoringService : WorkflowServiceBase
{
    const string STAGE1 = "Reading machine status";
    const string STAGE2 = "Checking utilization";
    const string STAGE3 = "Monitoring health metrics";

    public override string Name => nameof(MachineMonitoringService);
    public override ServicePriority Priority => ServicePriority.High;

    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var process1Delay = SharedMethods.ProcessingDuration();
        var process2Delay = SharedMethods.ProcessingDuration(tryForException: true);
        var process3Delay = SharedMethods.ProcessingDuration();

        await ExecuteStageAsync(STAGE1, process1Delay, cancellationToken);
        await ExecuteStageAsync(STAGE2, process2Delay, cancellationToken);
        await ExecuteStageAsync(STAGE3, process3Delay, cancellationToken);
    }
}

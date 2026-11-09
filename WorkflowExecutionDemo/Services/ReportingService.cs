using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo.Services;


/// <summary>
/// Generates production and performance reports.
/// Depends on ProductionService.
/// </summary>
public sealed class ReportingService : WorkflowServiceBase
{
    const string STAGE1 = "Collecting production data";
    const string STAGE2 = "Generating reports";
    const string STAGE3 = "Publishing reports";

    public override string Name => nameof(ReportingService);
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

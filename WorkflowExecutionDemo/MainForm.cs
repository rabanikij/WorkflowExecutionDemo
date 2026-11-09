using WorkflowExecutionDemo.Managers;
using WorkflowExecutionDemo.Models;
using WorkflowExecutionDemo.Services;
using WorkflowExecutionDemo.Shared;

namespace WorkflowExecutionDemo;


public partial class MainForm : Form
{
    private readonly WorkflowManager _manager = new();
    private CancellationTokenSource? _cts;

    public MainForm()
    {
        InitializeComponent();
        InitializeWorkflow();
    }

    /// <summary>
    /// Registers services and dependencies.
    /// </summary>
    private void InitializeWorkflow()
    {
        _manager.StatusChanged += OnStatusChanged;
        _manager.WorkflowStarted += OnWorkflowStarted;
        _manager.WorkflowCompleted += OnWorkflowCompleted;

        _manager.Register(new InventoryService());
        _manager.Register(new ProcurementService());
        _manager.Register(new MachineMonitoringService());
        _manager.Register(new ProductionPlanningService());
        _manager.Register(new ProductionService());
        _manager.Register(new QualityControlService());
        _manager.Register(new PackagingService());
        _manager.Register(new WarehouseService());
        _manager.Register(new ShippingService());
        _manager.Register(new ReportingService());

        _manager
            .DependsOn<ProductionPlanningService, InventoryService>()
            .DependsOn<ProductionService, ProductionPlanningService>()
            .DependsOn<QualityControlService, ProductionService>()
            .DependsOn<PackagingService, QualityControlService>()
            .DependsOn<WarehouseService, PackagingService>()
            .DependsOn<ShippingService, WarehouseService>()
            .DependsOn<ReportingService, ProductionService>();
    }

    private async void btnStart_Click(object? sender, EventArgs e)
    {
        btnStart.Enabled = false;
        _cts = new CancellationTokenSource();
        //Log(Constants.WorkflowStartedMessage);

        var stime = DateTime.Now;
        await _manager.ExecuteAsync(_cts.Token);
        var etime = DateTime.Now;
        Log($"{Constants.WorkflowDurationMessage} {(etime - stime)}");
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        txtLog.Clear();
    }

    /// <summary>
    /// Receives workflow updates from manager.
    /// </summary>
    private void OnStatusChanged(object? sender, StatusChangedEventArgs e)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => AppendLog(e.ToLogLine())));
            return;
        }

        AppendLog(e.ToLogLine());
    }

    /// <summary>
    /// When the Workflow Started
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnWorkflowStarted(object? sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() =>
            {
                Log(Constants.WorkflowStartedMessage);
                btnStart.Enabled = false;
            }));
            return;
        }

        //AppendLog($"[{TimeStampHelper.Now()}] [SYSTEM] Workflow started...");
        Log(Constants.WorkflowStartedMessage);
        btnStart.Enabled = false;
    }

    /// <summary>
    /// When the Workflow Completed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnWorkflowCompleted(object? sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() =>
            {
                Log(Constants.WorkflowCompletedMessage);
                btnStart.Enabled = true;
            }));
            return;
        }

        //AppendLog(Constants.WorkflowCompletedMessage);
        Log(Constants.WorkflowCompletedMessage);
        btnStart.Enabled = true;
    }

    private void AppendLog(string message)
    {
        txtLog.AppendText(message + Environment.NewLine);
    }

    private void Log(string message)
    {
        AppendLog($"\n[SYSTEM] [{TimeStampHelper.Now()}]: {message}\n");
    }
}

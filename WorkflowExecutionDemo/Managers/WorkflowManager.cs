using WorkflowExecutionDemo.Models;
using WorkflowExecutionDemo.Services;

namespace WorkflowExecutionDemo.Managers;


/// <summary>
/// Coordinates workflow service registration, dependencies, priorities, and execution.
/// </summary>
public class WorkflowManager
{
    private readonly List<WorkflowServiceBase> _services = new();
    private readonly Dictionary<Type, WorkflowServiceBase> _serviceMap = new();

    public event EventHandler<StatusChangedEventArgs>? StatusChanged;
    public event EventHandler? WorkflowStarted;
    public event EventHandler? WorkflowCompleted;

    /// <summary>
    /// Registers a service instance.
    /// </summary>
    public void Register<T>(T service) where T : WorkflowServiceBase
    {
        _services.Add(service);
        _serviceMap[typeof(T)] = service;

        service.StatusChanged += OnServiceStatusChanged;
    }

    /// <summary>
    /// Declares dependency between two services.
    /// </summary>
    public WorkflowManager DependsOn<TService, TDependency>()
        where TService : WorkflowServiceBase
        where TDependency : WorkflowServiceBase
    {
        var service = _serviceMap[typeof(TService)];
        var dependency = _serviceMap[typeof(TDependency)];

        service.AddDependency(dependency);

        return this;
    }

    /// <summary>
    /// Starts execution of all services respecting priority and dependencies.
    /// </summary>
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        WorkflowStarted?.Invoke(this, EventArgs.Empty);

        var grouped = _services
            .GroupBy(s => s.Priority)
            .OrderBy(p => p.Key); // High ? Low

        foreach (var group in grouped)
        {
            var tasks = group.Select(s => s.ExecuteAsync(cancellationToken));
            await Task.WhenAll(tasks);
        }

        WorkflowCompleted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Event forwarder from services to UI layer.
    /// </summary>
    private void OnServiceStatusChanged(object? sender, StatusChangedEventArgs e)
    {
        StatusChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Retrieves a registered service.
    /// </summary>
    public T GetService<T>() where T : WorkflowServiceBase
    {
        return (T)_serviceMap[typeof(T)];
    }
}

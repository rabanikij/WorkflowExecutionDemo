using System.Threading;
using System.Windows.Forms;

namespace WorkflowExecutionDemo.Shared;

/// <summary>
/// Central reusable helper methods used across workflow engine.
/// </summary>
public static class SharedMethods
{
    /// <summary>
    /// Builds a standardized log line for workflow events.
    /// </summary>
    public static string BuildLogLine(string timestamp, ServicePriority priority, string serviceName, string message)
    {
        return $"[{timestamp}] [{priority}] {serviceName} : {message}";
    }

    /// <summary>
    /// Safely invokes an action if not null (thread-safe UI usage).
    /// </summary>
    public static void SafeInvoke(Action? action)
    {
        action?.Invoke();
    }

    /// <summary>
    /// Combines service name and status into a readable format.
    /// </summary>
    public static string FormatStatus(string serviceName, WorkflowStatus status)
    {
        return $"{serviceName} - {status}";
    }

    public static int ProcessingDuration(bool tryForException = false)
    {
        if (tryForException)
        {
            bool FINDFAULT_IFTENS = DateTime.Now.Second % 10 == 0;  //Or Any logic DateTime.Now.Minute == 0 && DateTime.Now.Second == 0;
            if (FINDFAULT_IFTENS) return Constants.TOODELAYED;
        }

        Random _random = Random.Shared;
        return _random.Next(Constants.MinDelay, Constants.MaxDelay);
    }

    public static void CheckProcessState(int processDuration, string message)
    {
        //Validate too much delay in process stage of MachineMonitoringService
        if (processDuration > Constants.MaxDelay)
        {
            throw new Exception(Constants.WorkflowDeadLockedMessage);
        }
        //TRYING to
        //Simulating exception for even in ProductionService
        //Using more probability based logic Now.Second % 10 == 0 for simplicity
        bool FINDFAULT_IFTENS = (message.Contains("Manufacturing") && DateTime.Now.Second % 10 == 0);  //Or Any logic DateTime.Now.Minute == 0 && DateTime.Now.Second == 0;
        if (FINDFAULT_IFTENS)
        {
            throw new Exception(Constants.WorkflowFailedMessage);
        }
    }

}

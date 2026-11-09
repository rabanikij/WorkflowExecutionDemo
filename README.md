# WorkflowExecutionDemo
Demo of a process having multiple components working together to achieve a Workflow activity


# Spec:

- Target Framework: .NET 8, C#
- WinForm based UI
- Clean architecture followed
- SOLID principles followed
- Compact, professional coding style with XML documentation
- Dotnet project for multiple components execution with a multi-task class and a main window with button and text box. 
- Multi-task class with its constructor to propagate the creation status message to the textbox (dashboard console), similarly if status message updated then they propagate the new status message to the textbox in the main form.simulate the process time with random delay for each task type.
have got the scope of work or do you have any clarifications



# Scope:

- Dotnet project for multiple components execution with a multi-task manager class and a main window with button and text box. 
- Multi-task class with its constructor to propagate the creation status message to the output textbox (dashboard console)
- Similarly if status message updated then they propagate the new status message to the textbox in the main form.simulate the process time with random delay for each task type.


# Technical Spec:

## Workflow Manager: Responsible for creating and managing multiple task objects.
- Subscribes and starts all tasks concurrently.
- Receives status updates from every task.
- Forwards status updates to the Main Form through events.

## Individual Task Classes (Components): 
Each task class will do:
- Have its own constructor.
- Immediately report when creation started
- Report when Execution started asynchronously
- Simulate work using random delays
- Periodically send progress/status report updates
- Report completion



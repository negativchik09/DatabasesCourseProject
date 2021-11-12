using System;
using System.Windows.Input;

namespace BD_CourseProject.UI.Commands
{
    public class DefaultCommand : ICommand
    {
        public Action<object> ExecuteAction { get; set; }

        public Func<object, bool> CanExecuteFunc { get; set; }
        public DefaultCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc = null)
        {
            ExecuteAction = executeAction;
            CanExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecuteFunc?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            ExecuteAction(parameter);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
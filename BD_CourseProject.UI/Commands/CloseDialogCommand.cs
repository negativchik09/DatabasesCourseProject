using System;
using System.Windows;
using System.Windows.Input;

namespace BD_CourseProject.UI.Commands
{
    public class CloseDialogCommand : ICommand
    {
        public bool? DialogResult { get; set; }
        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = parameter as MemberView;

            window.DialogResult = DialogResult;
            
            window.Close();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
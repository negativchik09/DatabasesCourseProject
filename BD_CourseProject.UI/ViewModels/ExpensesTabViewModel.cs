using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.BL.Services;
using BD_CourseProject.UI.Commands;

namespace BD_CourseProject.UI.ViewModels
{
    public class ExpensesTabViewModel
    {
        private readonly IExpensesService _service;
        public ObservableCollection<ExpenseModel> Expenses { get; set; }

        public ExpensesTabViewModel()
        {
            _service = new ExpensesService();
            Expenses = new ObservableCollection<ExpenseModel>();
            UpdateDataFunction(null);
        }

        public ICommand UpdateData => new DefaultCommand(UpdateDataFunction);

        private void UpdateDataFunction(object obj)
        {
            Expenses.Clear();
            Expenses.AddRange(_service.ExpenseModels());
        }

        public ICommand AddExpense => new DefaultCommand(CreateExecute);
        
        private void CreateExecute(object? obj)
        {
            ShowExpenseCreateDialog(_service.AddExpense);
            UpdateDataFunction(null);
        }
        
        private void ShowExpenseCreateDialog(Action<ExpenseModel> action)
        {
            var window = new CreateExpense()
            {
                Owner = Application.Current.MainWindow,
                Members = _service.Members,
                ExpenseReasons = _service.Reasons,
                Date = DateTime.Today
            };

            if (window.ShowDialog() != true) return;
            action(new ExpenseModel()
            {
                MemberId = window.Member.Id,
                Date = window.Date,
                Sum = window.Sum,
                ReasonId = window.Reason.Id
            });
        }
    }
}
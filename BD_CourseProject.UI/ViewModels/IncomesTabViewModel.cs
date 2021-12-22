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
    public class IncomesTabViewModel
    {
        private readonly IIncomesService _service;
        public ObservableCollection<IncomeModel> Incomes { get; set; }

        public IncomesTabViewModel()
        {
            _service = new IncomesService();
            Incomes = new ObservableCollection<IncomeModel>();
            UpdateDataFunction(null);
        }

        public ICommand UpdateData => new DefaultCommand(UpdateDataFunction);

        private void UpdateDataFunction(object obj)
        {
            Incomes.Clear();
            Incomes.AddRange(_service.IncomeModels());
        }

        public ICommand AddIncome => new DefaultCommand(CreateExecute);
        
        private void CreateExecute(object? obj)
        {
            ShowIncomeCreateDialog(_service.AddIncome);
            UpdateDataFunction(null);
        }
        
        private void ShowIncomeCreateDialog(Action<IncomeModel> action)
        {
            var window = new CreateIncome()
            {
                Owner = Application.Current.MainWindow,
                Members = _service.Members,
                IncomeSources = _service.Sources,
                Date = DateTime.Today
            };

            if (window.ShowDialog() != true) return;
            action(new IncomeModel()
            {
                MemberId = window.Member.Id,
                Date = window.Date,
                Sum = window.Sum,
                SourceId = window.Source.Id
            });
        }
    }
}
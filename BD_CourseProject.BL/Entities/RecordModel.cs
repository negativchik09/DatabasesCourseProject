using System;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Entities
{
    public class RecordModel
    {
        public int MemberId { get; set; }
        public string Description { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public string DateString => Date.ToShortDateString();

        public RecordModel(Expense expense)
        {
            Date = expense.Date;
            Sum = -expense.Sum;
            Description = expense.Reason.Title;
        }

        public RecordModel(Income income)
        {
            Date = income.Date;
            Sum = income.Sum;
            Description = income.Source.Title;
        }

        public static RecordModel FromExpense(Expense expense) => new RecordModel(expense);

        public static RecordModel FromIncome(Income income) => new RecordModel(income);
    }
}
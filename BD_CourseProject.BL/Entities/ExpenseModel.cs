using System;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Entities
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public int ReasonId { get; set; }
        public string ReasonTitle { get; set; }
        public DateTime Date { get; set; }
        public string DateString => Date.ToShortDateString();
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public double Sum { get; set; }

        public ExpenseModel() { }

        internal ExpenseModel(Expense expense)
        {
            Id = expense.Id;
            ReasonId = expense.Reason.Id;
            ReasonTitle = expense.Reason.Title;
            Date = expense.Date;
            MemberId = expense.Member.Id;
            MemberName = expense.Member.FirstName + " " + expense.Member.LastName;
            Sum = expense.Sum;
        }
    }
}
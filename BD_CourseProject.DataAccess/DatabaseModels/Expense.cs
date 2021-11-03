using System;

namespace BD_CourseProject.DataAccess.DatabaseModels
{
    public record Expense : DatabaseModelBase
    {
        public Member Member { get; set; }
        public DateTime Date { get; set; }
        public ExpenseReason Reason { get; set; }
        public double Sum { get; set; }
    }
}
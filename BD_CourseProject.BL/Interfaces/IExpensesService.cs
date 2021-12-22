using System.Collections.Generic;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Interfaces
{
    public interface IExpensesService
    {
        public IEnumerable<ExpenseModel> ExpenseModels();
        public List<ExpenseReason> Reasons { get; }
        public List<Member> Members { get; }
        public void AddExpense(ExpenseModel model);
    }
}
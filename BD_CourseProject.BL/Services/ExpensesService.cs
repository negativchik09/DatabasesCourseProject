using System;
using System.Collections.Generic;
using System.Linq;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.DataAccess;
using BD_CourseProject.DataAccess.DatabaseModels;
using BD_CourseProject.DataAccess.Interfaces;

namespace BD_CourseProject.BL.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly IDatabaseService _db;

        public ExpensesService()
        {
            _db = DatabaseService.Instance;
        }

        public IEnumerable<ExpenseModel> ExpenseModels()
        {
            return _db.Expenses.Select(x => new ExpenseModel(x)).ToList();
        }

        public List<ExpenseReason> Reasons => _db.Reasons.ToList();
        public List<Member> Members => _db.Members.ToList();
        public void AddExpense(ExpenseModel model)
        {
            _db.Create(new Expense()
            {
                Date = model.Date,
                Member = _db.Members.First(x => x.Id == model.MemberId),
                Reason = new ExpenseReason(){ Id = model.ReasonId },
                Sum = model.Sum
            });
        }
    }
}
using System;
using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.DataAccess;
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

        public void GetByPeriod(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
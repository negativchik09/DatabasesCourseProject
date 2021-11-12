using System;

namespace BD_CourseProject.BL.Interfaces
{
    public interface IExpensesService
    {
        public void GetByPeriod(DateTime startDate, DateTime endDate);
    }
}
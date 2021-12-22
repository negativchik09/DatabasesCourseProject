using System.Collections.Generic;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Interfaces
{
    public interface IIncomesService
    {
        public IEnumerable<IncomeModel> IncomeModels();
        public List<IncomeSource> Sources { get; }
        
        public List<Member> Members { get; }
        public void AddIncome(IncomeModel model);
    }
}
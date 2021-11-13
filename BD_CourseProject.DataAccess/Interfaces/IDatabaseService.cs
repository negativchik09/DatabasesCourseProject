using System.Collections.Generic;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.DataAccess.Interfaces
{
    public interface IDatabaseService
    {
        public IEnumerable<Member> Members { get; }
        public void Create(Member member);
        public void Update(Member member);
        public void Delete(Member member);
        public IEnumerable<Income> Incomes { get; }
        public void Create(Income income);
        public IEnumerable<Expense> Expenses { get; }
        public void Create(Expense expense);
    }
}
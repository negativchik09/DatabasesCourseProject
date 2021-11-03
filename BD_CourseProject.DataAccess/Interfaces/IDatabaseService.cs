using System.Collections.Generic;
using System.Threading.Tasks;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.DataAccess.Interfaces
{
    public interface IDatabaseService
    {
        public IEnumerable<Member> Members { get; }
        public Task Create(Member member);
        public Task Update(Member member);
        public Task Delete(Member member);
        public IEnumerable<Income> Incomes { get; }
        public Task Create(Income income);
        public IEnumerable<Expense> Expenses { get; }
        public Task Create(Expense expense);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.DataAccess;
using BD_CourseProject.DataAccess.DatabaseModels;
using BD_CourseProject.DataAccess.Interfaces;

namespace BD_CourseProject.BL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IDatabaseService _db;

        public MemberService()
        {
            _db = DatabaseService.Instance;
        }

        public IEnumerable<MemberInfo> MemberInfos(MemberSearchFilter filter)
        {
            foreach (var element in _db.Members.Where(m => string.IsNullOrEmpty(filter.Query) 
                                                           || m.LastName.ToLower().Contains(filter.Query.ToLower())
                                                           || m.FirstName.ToLower().Contains(filter.Query.ToLower())
                                                           || m.Role.ToString().ToLower().Contains(filter.Query.ToLower())))
            {
                element.Incomes = element.Incomes.Where(i => i.Date >= filter.StartDate && i.Date <= filter.EndDate)
                    .ToList();
                element.Expenses = element.Expenses.Where(e => e.Date >= filter.StartDate && e.Date <= filter.EndDate)
                    .ToList();
                yield return new MemberInfo(element);
            }
        }

        public void AddMember(MemberData data)
        {
            _db.Create(new Member()
            {
                DateOfBirth = data.DateOfBirth.Date,
                LastName = data.LastName,
                FirstName = data.FirstName,
                Role = data.Role
            });
        }

        public void RemoveMember(MemberData data)
        {
            _db.Delete(new Member(){Id = data.Id});
        }

        public void UpdateMember(MemberData data)
        {
            _db.Update(new Member()
            {
                Id = data.Id,
                DateOfBirth = data.DateOfBirth.Date,
                LastName = data.LastName,
                FirstName = data.FirstName,
                Role = data.Role
            });
        }

        public IEnumerable<RecordModel> MemberStats(MemberStatsFilter filters)
        {
            Task<IEnumerable<Expense>> expenses = Task.Factory.StartNew(() =>
                _db.Expenses
                    .Where(e => e.Member.Id == filters.MemberId
                                && e.Date >= filters.StartDate
                                && e.Date <= filters.EndDate
                                && (e.Reason.Title.ToLower().Contains(filters.DescriptionSearch.ToLower())
                                    || string.IsNullOrEmpty(filters.DescriptionSearch))
                                )
                );
            Task<IEnumerable<Income>> incomes = Task.Factory.StartNew(() =>
                _db.Incomes
                    .Where(i => i.Member.Id == filters.MemberId
                                && i.Date >= filters.StartDate
                                && i.Date <= filters.EndDate
                                && (i.Source.Title.ToLower().Contains(filters.DescriptionSearch.ToLower())
                                    || string.IsNullOrEmpty(filters.DescriptionSearch))
                                )
                );
            return expenses.Result
                .Select(expense => new RecordModel(expense))
                .Concat(incomes.Result.Select(income => new RecordModel(income)))
                .ToList();
        }
    }
}
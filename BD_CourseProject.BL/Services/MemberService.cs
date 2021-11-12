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

        public IEnumerable<MemberInfo> MemberInfos(string searchCriteria)
        {
            return _db.Members.Where(m => m.FirstName.Contains(searchCriteria)
                                          || m.LastName.Contains(searchCriteria)
                                          || m.Role.ToString().Contains(searchCriteria)
                                          || string.IsNullOrEmpty(searchCriteria))
                .Select(m => new MemberInfo(m));
        }

        public void AddMember(MemberData data)
        {
            Task.Factory.StartNew(() => _db.Create(new Member()
            {
                DateOfBirth = data.DateOfBirth.Date,
                LastName = data.LastName,
                FirstName = data.FirstName,
                Role = data.Role
            }));
        }

        public void RemoveMember(MemberData data)
        {
            Task.Factory.StartNew(() => _db.Delete(new Member(){Id = data.Id}));
        }

        public void UpdateMember(MemberData data)
        {
            Task.Factory.StartNew(() => _db.Update(new Member()
            {
                DateOfBirth = data.DateOfBirth.Date,
                LastName = data.LastName,
                FirstName = data.FirstName,
                Role = data.Role
            }));
        }

        public IEnumerable<RecordModel> MemberStats(MemberStatsFilter filters)
        {
            Task<IEnumerable<Expense>> expenses = Task.Factory.StartNew(() =>
                _db.Expenses
                    .Where(e => e.Member.Id == filters.Member.Id
                                && e.Date > filters.StartDate
                                && e.Date < filters.EndDate
                                && (e.Reason.Title.Contains(filters.DescriptionSearch)
                                    || string.IsNullOrEmpty(filters.DescriptionSearch))
                                )
                );
            Task<IEnumerable<Income>> incomes = Task.Factory.StartNew(() =>
                _db.Incomes
                    .Where(i => i.Member.Id == filters.Member.Id
                                && i.Date > filters.StartDate
                                && i.Date < filters.EndDate
                                && (i.Source.Title.Contains(filters.DescriptionSearch)
                                    || string.IsNullOrEmpty(filters.DescriptionSearch))
                                )
                );
            return expenses.Result
                .Select(expense => new RecordModel(expense))
                .Concat(incomes.Result
                    .Select(income => new RecordModel(income)))
                .ToList();
        }
    }
}
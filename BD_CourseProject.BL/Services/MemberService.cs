using System;
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
            Func<Member, bool> isLastNameContains = (m) => m.LastName.ToLower().Contains(searchCriteria.ToLower());
            Func<Member, bool> isFirstNameContains = (m) => m.FirstName.ToLower().Contains(searchCriteria.ToLower());
            Func<Member, bool> isRoleContains = (m) => m.Role.ToString().ToLower().Contains(searchCriteria.ToLower());
            return _db.Members.Where(m => string.IsNullOrEmpty(searchCriteria) 
                                          || isFirstNameContains(m)
                                          || isLastNameContains(m)
                                          || isRoleContains(m))
                .Select(m => new MemberInfo(m));
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
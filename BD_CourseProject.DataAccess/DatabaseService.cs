using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD_CourseProject.DataAccess.DatabaseModels;
using BD_CourseProject.DataAccess.DTO;
using BD_CourseProject.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BD_CourseProject.DataAccess
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext _ctx;
        
        private class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
        {
            public DatabaseContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                optionsBuilder.UseSqlServer(args[0]);

                return new DatabaseContext(optionsBuilder.Options);
            }
        }

        public DatabaseService(string connectionString)
        {
            _ctx = new DatabaseContextFactory().CreateDbContext(new string[] {connectionString});
        }

        public IEnumerable<Member> Members => _ctx.Members.Select(x => new Member()
        {
            DateOfBirth = x.DateOfBirth,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Id = x.Id,
            Role = x.Role
        }).ToList();
        
        public async void Create(Member member)
        {
            await _ctx.AddAsync(new MemberDomain()
            {
                DateOfBirth = member.DateOfBirth,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Role = member.Role
            });
            await _ctx.SaveChangesAsync();
        }

        public async void Update(Member member)
        {
            var mem = _ctx.Members.First(x => x.Id == member.Id);
            mem.FirstName = member.FirstName;
            mem.LastName = member.LastName;
            mem.Role = member.Role;
            mem.DateOfBirth = member.DateOfBirth;
            await _ctx.SaveChangesAsync();
        }

        public async void Delete(Member member)
        {
            _ctx.Members.Remove(_ctx.Members.First(x => x.Id == member.Id));
            await _ctx.SaveChangesAsync();
        }

        public Member GetMemberById(Func<Member, bool> predicate)
        {
            return _ctx.Members.Select(memberDomain => new Member()
            {
                Id = memberDomain.Id,
                DateOfBirth = memberDomain.DateOfBirth,
                FirstName = memberDomain.FirstName,
                LastName = memberDomain.LastName,
                Role = memberDomain.Role
            }).First(predicate);
        }

        public IEnumerable<Income> GetIncomesByMember(Member member)
        {
            return _ctx.Incomes.Where(x => x.MemberId == member.Id)
                .Select(x => new Income()
                {
                    Id = x.Id,
                    Member = GetMemberById(mem => mem.Id == x.MemberId),
                    Source = x.Source,
                    Date = x.Date,
                    Sum = x.Sum
                });
        }

        public IEnumerable<Expense> GetExpensesByMember(Member member)
        {
            return _ctx.Expenses.Where(x => x.MemberId == member.Id).Select(exp => new Expense()
                {
                    Id = exp.Id,
                    Member = member,
                    Date = exp.Date,
                    Reason = exp.Reason,
                    Sum = exp.Sum
                });
        }

        public IEnumerable<Income> Incomes => _ctx.Incomes.Select(x => new Income()
        {
            Id = x.Id,
            Member = GetMemberById(member => member.Id == x.MemberId),
            Source = x.Source,
            Date = x.Date,
            Sum = x.Sum
        }).ToList();
        public void Create(Income income)
        {
            _ctx.Incomes.Add(new IncomeDomain()
            {
                MemberId = income.Member.Id,
                Date = income.Date,
                Source = income.Source,
                Sum = income.Sum
            });
            _ctx.SaveChangesAsync();
        }

        public IEnumerable<Expense> Expenses => _ctx.Expenses.Select(x => new Expense()
        {
            Id = x.Id,
            Member = GetMemberById(member => member.Id == x.MemberId),
            Reason = x.Reason,
            Date = x.Date,
            Sum = x.Sum
        }).ToList();
        public void Create(Expense expense)
        {
            _ctx.Expenses.Add(new ExpenseDomain()
            {
                MemberId = expense.Member.Id,
                Date = expense.Date,
                Reason = expense.Reason,
                Sum = expense.Sum
            });
            _ctx.SaveChangesAsync();
        }
    }
}
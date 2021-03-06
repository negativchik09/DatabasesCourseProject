using System;
using System.Collections.Generic;
using System.Linq;
using BD_CourseProject.DataAccess.DatabaseModels;
using BD_CourseProject.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BD_CourseProject.DataAccess
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext _ctx;

        public static DatabaseService Instance { get; }

        static DatabaseService()
        {
            Instance = new DatabaseService();
        }

        private DatabaseService()
        {
            _ctx = new DatabaseContextFactory().CreateDbContext(new string[]{});
        }

        public IEnumerable<Member> Members => _ctx.Members
            .Include(x => x.Expenses)
            .Include(x => x.Incomes);
        
        public void Create(Member member)
        {
            _ctx.Add(member);
            _ctx.SaveChanges();
        }

        public void Update(Member member)
        {
            var entity = _ctx.Members.First(x => x.Id == member.Id);
            entity.Role = member.Role;
            entity.FirstName = member.FirstName;
            entity.LastName = member.LastName;
            entity.DateOfBirth = member.DateOfBirth;
            _ctx.Members.Update(entity);
            _ctx.SaveChanges();
        }

        public void Delete(Member member)
        {
            _ctx.Members.Remove(_ctx.Members.First(x => x.Id == member.Id));
            _ctx.SaveChanges();
        }

        public IEnumerable<Income> Incomes => _ctx.Incomes
            .Include(x => x.Member)
            .Include(x => x.Source);
        
        public void Create(Income income)
        {
            var source = _ctx.IncomeSources.FirstOrDefault(x => x.Id == income.Source.Id);
            if (source == null)
            {
                throw new ArgumentException("Invalid source Id");
            }
            income.Source = source;
            _ctx.Add(income);
            _ctx.SaveChanges();
        }

        public IEnumerable<IncomeSource> Sources => _ctx.IncomeSources;

        public IEnumerable<Expense> Expenses => _ctx.Expenses
            .Include(x => x.Member)
            .Include(x => x.Reason);
        
        public void Create(Expense expense)
        {
            var reason = _ctx.ExpenseReasons.FirstOrDefault(x => x.Id == expense.Reason.Id);
            if (reason == null)
            {
                throw new ArgumentException("invalid reason Id");
            }

            expense.Reason = reason;

            _ctx.Add(expense);
            _ctx.SaveChanges();
        }
        
        public IEnumerable<ExpenseReason> Reasons => _ctx.ExpenseReasons;
    }
}
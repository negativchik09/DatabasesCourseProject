using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD_CourseProject.DataAccess.DatabaseModels;
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
                optionsBuilder.UseSqlServer(
                    "Server=localhost\\SQLEXPRESS;Database=BD_CourseProject;Trusted_Connection=True;",
                    builder => builder.MigrationsAssembly("BD_CourseProject.DataAccess")
                    );

                return new DatabaseContext(optionsBuilder.Options);
            }
        }
        
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
        
        public async Task Create(Member member)
        {
            await _ctx.AddAsync(member);
            await _ctx.SaveChangesAsync();
        }

        public async Task Update(Member member)
        {
            _ctx.Members.Update(_ctx.Members.First(x => x.Id == member.Id));
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(Member member)
        {
            _ctx.Members.Remove(_ctx.Members.First(x => x.Id == member.Id));
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<Income> Incomes => _ctx.Incomes
            .Include(x => x.Member)
            .Include(x => x.Source);
        
        public async Task Create(Income income)
        {
            var source = _ctx.IncomeSources.FirstOrDefault(x => x.Id == income.Source.Id);
            if (source == null)
            {
                throw new ArgumentException("invalid source Id");
            }

            income.Source = source;

            await _ctx.AddAsync(income);
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<Expense> Expenses => _ctx.Expenses
            .Include(x => x.Member)
            .Include(x => x.Reason);
        
        public async Task Create(Expense expense)
        {
            var reason = _ctx.ExpenseReasons.FirstOrDefault(x => x.Id == expense.Reason.Id);
            if (reason == null)
            {
                throw new ArgumentException("invalid source Id");
            }

            expense.Reason = reason;

            await _ctx.AddAsync(expense);
            await _ctx.SaveChangesAsync();
        }
    }
}
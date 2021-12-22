using BD_CourseProject.DataAccess.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace BD_CourseProject.DataAccess
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseReason> ExpenseReasons { get; set; }
        public DbSet<IncomeSource> IncomeSources { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasMany(member => member.Incomes)
                .WithOne(income => income.Member)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Member>()
                .HasMany(member => member.Expenses)
                .WithOne(expense => expense.Member)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Income>()
                .HasOne(income => income.Source)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Expense>()
                .HasOne(expense => expense.Reason)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
    }
}
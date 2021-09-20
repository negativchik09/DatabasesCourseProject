using BD_CourseProject.DataAccess.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace BD_CourseProject.DataAccess
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<MemberDomain> Members { get; set; }
        public DbSet<IncomeDomain> Incomes { get; set; }
        public DbSet<ExpenseDomain> Expenses { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    }
}
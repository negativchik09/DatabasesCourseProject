using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BD_CourseProject.DataAccess
{
    internal class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
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
}
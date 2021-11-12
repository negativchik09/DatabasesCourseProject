using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.DataAccess;
using BD_CourseProject.DataAccess.Interfaces;

namespace BD_CourseProject.BL.Services
{
    public class IncomesService : IIncomesService
    {
        private readonly IDatabaseService _db;

        public IncomesService()
        {
            _db = DatabaseService.Instance;
        }
    }
}
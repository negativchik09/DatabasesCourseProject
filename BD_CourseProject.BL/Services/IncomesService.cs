using System.Collections.Generic;
using System.Linq;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.DataAccess;
using BD_CourseProject.DataAccess.DatabaseModels;
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

        public IEnumerable<IncomeModel> IncomeModels()
        {
            return _db.Incomes.Select(i => new IncomeModel(i));
        }

        public List<IncomeSource> Sources => _db.Sources.ToList();

        public List<Member> Members => _db.Members.ToList();

        public void AddIncome(IncomeModel model)
        {
            _db.Create(new Income()
            {
                Date = model.Date,
                Member = _db.Members.First(x => x.Id == model.MemberId),
                Source = new IncomeSource(){ Id = model.SourceId },
                Sum = model.Sum
            });
        }
    }
}
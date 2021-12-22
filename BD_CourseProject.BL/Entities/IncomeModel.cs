using System;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Entities
{
    public class IncomeModel
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string SourceTitle { get; set; }
        public DateTime Date { get; set; }
        public string DateString => Date.ToShortDateString();
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public double Sum { get; set; }

        public IncomeModel() { }

        internal IncomeModel(Income income)
        {
            Id = income.Id;
            SourceId = income.Source.Id;
            SourceTitle = income.Source.Title;
            Date = income.Date;
            MemberId = income.Member.Id;
            MemberName = income.Member.FirstName + " " + income.Member.LastName;
            Sum = income.Sum;
        }
    }
}
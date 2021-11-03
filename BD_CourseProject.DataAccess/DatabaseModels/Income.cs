using System;

namespace BD_CourseProject.DataAccess.DatabaseModels
{
    public record Income : DatabaseModelBase
    {
        public Member Member { get; set; }
        public DateTime Date { get; set; }
        public IncomeSource Source { get; set; }
        public double Sum { get; set; }
    }
}
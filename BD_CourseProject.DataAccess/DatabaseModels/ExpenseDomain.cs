using System;

namespace BD_CourseProject.DataAccess.DatabaseModels
{
    internal record ExpenseDomain : DatabaseModelBase
    {
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public double Sum { get; set; }
    }
}
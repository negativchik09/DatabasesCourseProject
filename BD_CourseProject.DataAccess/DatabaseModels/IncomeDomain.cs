using System;

namespace BD_CourseProject.DataAccess.DatabaseModels
{
    internal record IncomeDomain : DatabaseModelBase
    {
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public double Sum { get; set; }
    }
}
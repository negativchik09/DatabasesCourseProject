using System;

namespace BD_CourseProject.BL.Entities
{
    public class MemberSearchFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Query { get; set; }
        public MemberSearchFilter(DateTime startDate, DateTime endDate, string query)
        {
            StartDate = startDate;
            EndDate = endDate;
            Query = query;
        }
    }
}
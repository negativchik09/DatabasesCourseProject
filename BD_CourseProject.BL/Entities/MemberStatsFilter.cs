using System;

namespace BD_CourseProject.BL.Entities
{
    public class MemberStatsFilter
    {
        public MemberStatsFilter(DateTime startDate, DateTime endDate, string descriptionSearch)
        {
            StartDate = startDate;
            EndDate = endDate;
            DescriptionSearch = descriptionSearch;
        }

        public MemberStatsFilter(DateTime startDate, DateTime endDate) 
            : this(startDate, endDate, String.Empty){}

        public MemberStatsFilter() : this(DateTime.MinValue, DateTime.MaxValue) {}
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DescriptionSearch { get; set; }
        public MemberData Member { get; set; }
    }
}
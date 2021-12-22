using System;

namespace BD_CourseProject.BL.Entities
{
    public class MemberStatsFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DescriptionSearch { get; set; }
        public int MemberId { get; set; }
        public MemberStatsFilter(int memberId, DateTime startDate, DateTime endDate, string descriptionSearch)
        {
            MemberId = memberId;
            StartDate = startDate;
            EndDate = endDate;
            DescriptionSearch = descriptionSearch;
        }
        public MemberStatsFilter(int memberId) : this(memberId, DateTime.MinValue, DateTime.MaxValue, string.Empty) {}
    }
}
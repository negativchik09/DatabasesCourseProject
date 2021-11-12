using System.Collections.Generic;
using BD_CourseProject.BL.Entities;

namespace BD_CourseProject.BL.Interfaces
{
    public interface IMemberService
    {
        public IEnumerable<MemberInfo> MemberInfos(string searchCriteria);
        public void AddMember(MemberData data);
        public void RemoveMember(MemberData data);
        public void UpdateMember(MemberData data);
        public IEnumerable<RecordModel> MemberStats(MemberStatsFilter filters);
    }
}
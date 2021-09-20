using System;

namespace BD_CourseProject.DataAccess.DatabaseModels
{
    internal record MemberDomain : DatabaseModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FamilyRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
using System;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.DataAccess.DTO
{
    public record Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FamilyRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
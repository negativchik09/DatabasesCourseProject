﻿using System;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Entities
{
    public class MemberData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FamilyRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateString => DateOfBirth.ToShortDateString();
        public MemberData(){}

        internal MemberData(Member member)
        {
            FirstName = member.FirstName;
            LastName = member.LastName;
            Role = member.Role;
            DateOfBirth = member.DateOfBirth.Date;
        }
    }
}
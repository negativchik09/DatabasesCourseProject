using System;
using System.Collections.Generic;

namespace BD_CourseProject.DataAccess.DatabaseModels
{
    public record Member : DatabaseModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FamilyRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.UI
{
    public partial class MemberView : Window
    {
        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public string FirstName 
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }
        public string LastName 
        {
            get => (string)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }
        public FamilyRole Role
        {
            get => (FamilyRole)GetValue(RoleProperty);
            set => SetValue(RoleProperty, value);
        }
        public DateTime DateOfBirth
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public Dictionary<FamilyRole, string> FamilyRoleTypeValues
        {
            get => (Dictionary<FamilyRole, string>) GetValue(EnumProperty);
            set => SetValue(EnumProperty, value);
        }
            

        public static readonly DependencyProperty IdProperty = 
            DependencyProperty.Register(
                nameof(Id),
                typeof(int),
                typeof(MemberView));
        public static readonly DependencyProperty FirstNameProperty = 
            DependencyProperty.Register(
                nameof(FirstName),
                typeof(string),
                typeof(MemberView));
        public static readonly DependencyProperty LastNameProperty = 
            DependencyProperty.Register(
                nameof(LastName),
                typeof(string),
                typeof(MemberView));
        public static readonly DependencyProperty RoleProperty = 
            DependencyProperty.Register(
                nameof(Role),
                typeof(FamilyRole),
                typeof(MemberView));
        public static readonly DependencyProperty DateProperty = 
            DependencyProperty.Register(
                nameof(DateOfBirth),
                typeof(DateTime),
                typeof(MemberView));
        public static readonly DependencyProperty EnumProperty = 
            DependencyProperty.Register(
                nameof(FamilyRoleTypeValues),
                typeof(Dictionary<FamilyRole, string>),
                typeof(MemberView));
        public MemberView()
        {
            FamilyRoleTypeValues = new Dictionary<FamilyRole, string>(
                new List<KeyValuePair<FamilyRole, string>>()
                {
                    new KeyValuePair<FamilyRole, string>(FamilyRole.Child, "Child"),
                    new KeyValuePair<FamilyRole, string>(FamilyRole.Father, "Father"),
                    new KeyValuePair<FamilyRole, string>(FamilyRole.Mother, "Mother"),
                    new KeyValuePair<FamilyRole, string>(FamilyRole.Grandfather, "Grandfather"),
                    new KeyValuePair<FamilyRole, string>(FamilyRole.Grandmother, "Grandmother")
                });
            InitializeComponent();
        }
    }
}
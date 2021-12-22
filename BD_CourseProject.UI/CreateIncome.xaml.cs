using System;
using System.Collections.Generic;
using System.Windows;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.UI
{
    public partial class CreateIncome
    {
        public double Sum 
        {
            get => (double)GetValue(SumProperty);
            set => SetValue(SumProperty, value);
        }
        public IncomeSource Source 
        {
            get => (IncomeSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        public Member Member
        {
            get => (Member)GetValue(MemberProperty);
            set => SetValue(MemberProperty, value);
        }
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        public List<IncomeSource> IncomeSources
        {
            get => (List<IncomeSource>) GetValue(IncomeSourcesProperty);
            set => SetValue(IncomeSourcesProperty, value);
        }
        public List<Member> Members
        {
            get => (List<Member>) GetValue(MembersProperty);
            set => SetValue(MembersProperty, value);
        }
        public static readonly DependencyProperty SumProperty = 
            DependencyProperty.Register(
                nameof(Sum),
                typeof(double),
                typeof(CreateIncome));
        public static readonly DependencyProperty SourceProperty = 
            DependencyProperty.Register(
                nameof(Source),
                typeof(IncomeSource),
                typeof(CreateIncome));
        public static readonly DependencyProperty MemberProperty = 
            DependencyProperty.Register(
                nameof(Member),
                typeof(Member),
                typeof(CreateIncome));
        public static readonly DependencyProperty DateProperty = 
            DependencyProperty.Register(
                nameof(Date),
                typeof(DateTime),
                typeof(CreateIncome));
        public static readonly DependencyProperty IncomeSourcesProperty =
            DependencyProperty.Register(
                nameof(IncomeSources),
                typeof(List<IncomeSource>),
                typeof(CreateIncome));
        public static readonly DependencyProperty MembersProperty =
            DependencyProperty.Register(
                nameof(Members),
                typeof(List<Member>),
                typeof(CreateIncome));

        public CreateIncome()
        {
            InitializeComponent();
        }
    }
}
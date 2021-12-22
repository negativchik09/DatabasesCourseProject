using System;
using System.Collections.Generic;
using System.Windows;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.UI
{
    public partial class CreateExpense
    {
        public double Sum 
        {
            get => (double)GetValue(SumProperty);
            set => SetValue(SumProperty, value);
        }
        public ExpenseReason Reason 
        {
            get => (ExpenseReason)GetValue(SourceProperty);
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
        public List<ExpenseReason> ExpenseReasons
        {
            get => (List<ExpenseReason>) GetValue(ExpenseReasonsProperty);
            set => SetValue(ExpenseReasonsProperty, value);
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
                typeof(CreateExpense));
        public static readonly DependencyProperty SourceProperty = 
            DependencyProperty.Register(
                nameof(Reason),
                typeof(ExpenseReason),
                typeof(CreateExpense));
        public static readonly DependencyProperty MemberProperty = 
            DependencyProperty.Register(
                nameof(Member),
                typeof(Member),
                typeof(CreateExpense));
        public static readonly DependencyProperty DateProperty = 
            DependencyProperty.Register(
                nameof(Date),
                typeof(DateTime),
                typeof(CreateExpense));
        public static readonly DependencyProperty ExpenseReasonsProperty =
            DependencyProperty.Register(
                nameof(ExpenseReasons),
                typeof(List<ExpenseReason>),
                typeof(CreateExpense));
        public static readonly DependencyProperty MembersProperty =
            DependencyProperty.Register(
                nameof(Members),
                typeof(List<Member>),
                typeof(CreateExpense));

        public CreateExpense()
        {
            InitializeComponent();
        }
    }
}
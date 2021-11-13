using System.Linq;
using BD_CourseProject.DataAccess.DatabaseModels;

namespace BD_CourseProject.BL.Entities
{
    public class MemberInfo : MemberData
    {
        public double ExpensesSum { get; set; }
        public double IncomesSum { get; set; }
        public double Marge => IncomesSum - ExpensesSum;
        public MemberInfo(){}

        internal MemberInfo(Member member) : base(member)
        {
            ExpensesSum = member.Expenses.Sum(x => x.Sum);
            IncomesSum = member.Incomes.Sum(x => x.Sum);
        }
    }
}
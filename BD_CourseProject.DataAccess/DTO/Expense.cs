using System;

namespace BD_CourseProject.DataAccess.DTO
{
    public record Expense
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public double Sum { get; set; }
    }
}
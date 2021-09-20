using System;

namespace BD_CourseProject.DataAccess.DTO
{
    public record Income
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public double Sum { get; set; }
    }
}
using ArelApp.Entities.Concrete;
using System.Collections.Generic;


namespace ArelApp.UI.Models
{
    public class ExamViewModel
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public string AcademicianId { get; set; }
        public int Midterm { get; set; }
        public int Final { get; set; }
        public int LectureId { get; set; }
        public List<Lecture> Lectures { get; set; }
        public List<Exam> Exams { get; set; }
    }
}

using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class ExamModel
    {
        public string Name { get; set; }
        public int ExamId { get; set; }
        public int Midterm { get; set; }
        public int Final { get; set; }
    }

    public class LectureModel
    {
        public int LectureId { get; set; }
        public List<ExamModel> Exams { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}

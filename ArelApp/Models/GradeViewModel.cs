using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.UI.Models
{
    public class GradeViewModel
    {

        public GradeViewModel()
        {
            LectureswithDepartment = new List<GradeModel>();
        }

        public List<GradeModel> LectureswithDepartment { get; set; }

    }


    public class GradeModel
    {
        public GradeModel()
        {
            LecturesStudents = new List<StudentViewModel>();
        }
        public string DepartmentName { get; set; }
        public List<StudentViewModel> LecturesStudents { get; set; }
    }

    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Students = new List<StudentModel>();
        }
        public string LectureName { get; set; }
        public int LectureId { get; set; }
        public List<StudentModel> Students { get; set; }
    }

    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ExamId { get; set; }
        public int Midterm { get; set; }
        public int Final { get; set; }
    }

}

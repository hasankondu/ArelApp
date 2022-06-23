using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class ApproveModel
    {
        public ApproveModel()
        {
            StudentswithLectures = new List<StudentApproveModel>();
        }
        public List<StudentApproveModel> StudentswithLectures { get; set; }

    }

   

    public class StudentApproveModel
    {

        public StudentApproveModel()
        {
            StudentsLectures = new List<LectureApproveModel>();
        }

        public string DepartmentName { get; set; }
        public List<LectureApproveModel> StudentsLectures { get; set; }


    }
    public class LectureApproveModel
    {

        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public List<Lecture> Lectures { get; set; }


    }

}

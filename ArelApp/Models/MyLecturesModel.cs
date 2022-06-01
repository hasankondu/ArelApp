using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class MyLecturesModel
    {
        public MyLecturesModel()
        {
            LectureswithDepartment = new List<MyLectureViewModel>();
        }
        
        public List<MyLectureViewModel> LectureswithDepartment { get; set; }
    }

    public class MyLectureViewModel
    {
        public MyLectureViewModel()
        {
            Lectures = new List<Lecture>();
        }
        public string Relation { get; set; }
        public string DepartmentName { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}

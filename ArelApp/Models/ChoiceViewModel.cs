using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class ChoiceViewModel
    {
        public ChoiceViewModel()
        {
            LectureswithDepartment = new List<LectureChoiceViewModel>();
        }

        public List<LectureChoiceViewModel> LectureswithDepartment { get; set; }
    }

    public class LectureChoiceViewModel
    {
        public LectureChoiceViewModel()
        {
            Lectures = new List<Lecture>();
        }
        public string DepartmentName { get; set; }
        public List<Lecture> Lectures { get; set; }
    }


}

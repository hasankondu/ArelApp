using ArelApp.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ArelApp.UI.Models
{
    public class LectureViewModel
    {
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Credit { get; set; }

        public List<Lecture> Lectures { get; set; }
        public List<Department> Departments { get; set; }
        public List<User> Users { get; set; }
        public int DepartmentId { get; set; }
        public User SelectedAcademician { get; set; }
        public int UserId { get; set; }
        public IEnumerable<SelectListItem> LecturesDepartment { get; set; }
        public IEnumerable<SelectListItem> LecturesAcademician { get; set; }


    }


}

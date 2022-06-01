using ArelApp.Entities.Concrete;
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
        public int DepartmentId { get; set; }


    }


}

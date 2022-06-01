using ArelApp.Entities.Concrete;
using System.Collections.Generic;


namespace ArelApp.UI.Models
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<Department> Departments { get; set; }
    }
}

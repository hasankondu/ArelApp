using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class UserUpdateModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public IList<string> SelectedRoles { get; set; }
        public List<Department> SelectedDepartments { get; set; }
        public List<Department> Departments { get; set; }
        public List<Role> Roles { get; set; }
    }
}

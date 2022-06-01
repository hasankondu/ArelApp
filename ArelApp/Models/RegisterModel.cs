using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            Departments = new List<Department>();
            Roles = new List<Role>();
        }
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
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public IList<string> SelectedRoles { get; set; }
        public List<Department> SelectedDepartments { get; set; }
        public List<Department> Departments { get; set; }
        public List<Role> Roles { get; set; }
    }
}

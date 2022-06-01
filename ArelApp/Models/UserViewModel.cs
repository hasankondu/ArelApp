using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.UI.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Users = new List<User>();
            Departments = new List<Department>();
            Roles = new List<Role>();

        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IList<string> RoleNames { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public List<User> Users { get; set; }
        public List<Department> Departments { get; set; }
        public List<Role> Roles { get; set; }
    }
}

using ArelApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ArelApp.Entities.Concrete
{
    public class User : IdentityUser<int>, IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<UserDepartment> UserDepartments { get; set; }

        public virtual ICollection<UserLecture> UserLectures { get; set; }
    }
}

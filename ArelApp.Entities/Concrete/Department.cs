using ArelApp.Core.Entities;
using System.Collections.Generic;

namespace ArelApp.Entities.Concrete
{
    public class Department : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<UserDepartment> UserDepartments { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}

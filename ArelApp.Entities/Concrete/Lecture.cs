using ArelApp.Core.Entities;
using System.Collections.Generic;

namespace ArelApp.Entities.Concrete
{
    public class Lecture : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Credit { get; set; }
        public string AcademicianId { get; set; }
        public string StudentId { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


        public virtual ICollection<UserLecture> UserLectures { get; set; }
        public virtual List<Exam> Exams { get; set; }

    }
}

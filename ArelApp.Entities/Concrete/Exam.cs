using ArelApp.Core.Entities;
using System.Collections.Generic;

namespace ArelApp.Entities.Concrete
{
    public class Exam : EntityBase, IEntity
    {

        public string StudentId { get; set; }
        public int Midterm { get; set; }
        public int Final { get; set; }

        public int LectureId { get; set; }
        public virtual Lecture Lecture { get; set; }



    }
}

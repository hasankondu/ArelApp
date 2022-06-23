using ArelApp.Core.Entities;

namespace ArelApp.Entities.Concrete
{
    public class UserExam
    {
        public class UserLecture : IEntity
        {
            public int ExamId { get; set; }
            public virtual Exam Exam { get; set; }

            public int UserId { get; set; }
            public virtual User User { get; set; }

        }
    }
}

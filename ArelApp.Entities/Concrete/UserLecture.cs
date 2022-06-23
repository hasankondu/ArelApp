using ArelApp.Core.Entities;

namespace ArelApp.Entities.Concrete
{
    public class UserLecture :  IEntity
    {
        public int LectureId { get; set; }
        public virtual Lecture Lecture { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public EnumApprovalStatus ApprovalStatus { get; set; }

        
    }
    public enum EnumApprovalStatus
    {
        Approved = 0,
        Denied = 1,
        Pending = 2,
        NotSubmitted = 3,
        Teaching = 4

    }
}

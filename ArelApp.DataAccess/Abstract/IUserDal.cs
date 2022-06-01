using ArelApp.Core.DataAccess;
using ArelApp.Entities.Concrete;
using static ArelApp.Entities.Concrete.UserLecture;

namespace ArelApp.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {

        User GetThatUsersDepartments(int id);
        User GetThatUsersLectures(int id);
        User GetThatAcademiciansStudents(int id);
        void UpdateUserDepartments(User entity, int[] DepartmentIds);
        void UpdateUserLectures(User entity, int[] LectureIds, EnumApprovalStatus approvalStatus);

        

    }
}

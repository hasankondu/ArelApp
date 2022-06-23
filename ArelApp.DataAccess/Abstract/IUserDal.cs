using ArelApp.Core.DataAccess;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {

        User GetThatUsersDepartments(int id);
        User GetThatUsersLectures(int id);
        User GetThatAcademiciansStudents(int id);
        void UpdateUserDepartments(User entity, int[] DepartmentIds);
        void UpdateUserLectures(User entity, int[] LectureIds, EnumApprovalStatus approvalStatus);
        void AssignUserToLecture(User entity, int LectureId, EnumApprovalStatus approvalStatus);
        void ReAssignAcademicianToLecture(User oldacademician, int academicianid, int LectureId, EnumApprovalStatus approvalStatus);
        List<User> GetThatUsersByDepartmentId(int departmentid);

        User GetThatStudentsAcademicians(int id);
    }
}

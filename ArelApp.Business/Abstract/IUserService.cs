using ArelApp.Entities.Concrete;
using ArelApp.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static ArelApp.Entities.Concrete.UserLecture;

namespace ArelApp.Business.Abstract
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(User user,string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task Delete(User user);
        Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
        Task<SignInResult> PasswordSignInAsync(string username,string password);
        Task<bool> IsInRoleAsync(User user, string role);
        Task SignOutAsync();
        Task<User> FindByNameAsync(string userName);
        Task<User> FindByIdAsync(string id);
        List<User> List();
        Task<IList<string>> GetRolesAsync(User user);
        Task<IList<User>> GetUsersInRoleAsync(string roleName);
        User GetThatUsersDepartments(int id);
        User GetThatAcademiciansStudents(int id);
        User GetThatUsersLectures(int id);
        void UpdateUserDepartments(User entity, int[] DepartmentIds);
        void UpdateUserLectures(User entity, int[] LectureIds, EnumApprovalStatus approvalStatus);
        void AssignUserToLecture(User entity, int LectureId, EnumApprovalStatus approvalStatus);
        string GetUserId(ClaimsPrincipal principal);
        void ReAssignAcademicianToLecture(User oldacademician, int academicianid, int LectureId, EnumApprovalStatus approvalStatus);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        List<User> GetThatUsersByDepartmentId(int departmentid);
        User GetThatStudentsAcademicians(int id);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

    }
}

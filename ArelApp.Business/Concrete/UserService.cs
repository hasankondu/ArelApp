using ArelApp.Business.Abstract;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static ArelApp.Entities.Concrete.UserLecture;

namespace ArelApp.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<User> _userService;
        private readonly SignInManager<User> _signInService;
        private IUserDal _userDal;
        public UserService(UserManager<User> userService, IRoleService roleService, IUserDal userDal, SignInManager<User> signInService)
        {

            _userService = userService;
            _roleService = roleService;
            _userDal = userDal;
            _signInService = signInService;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userService.CreateAsync(user, password);

        }


        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userService.AddToRoleAsync(user, role);
          


        }

        
        public async Task Delete(User user)
        {
            await _userService.DeleteAsync(user);
           
        }


        public async Task<User> FindByIdAsync(string id)
        {
            return await _userService.FindByIdAsync(id);
        }


        public User GetThatUsersDepartments(int id)
        {
           return _userDal.GetThatUsersDepartments(id);
        }

        public List<User> List()
        {
            return _userService.Users.ToList();
        }



        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userService.UpdateAsync(user);
            
            
        }
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            return await _userService.GetUsersInRoleAsync(roleName);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return _userService.GetRolesAsync(user);
        }

        public void UpdateUserDepartments(User entity, int[] DepartmentIds)
        {
            _userDal.UpdateUserDepartments(entity,DepartmentIds);
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _userService.AddToRolesAsync(user,roles);
        }

        public async Task<IdentityResult> ChangePassword(User user,string currentPassword,string newPassword)
        {
            return await _userService.ChangePasswordAsync( user,  currentPassword,  newPassword);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _userService.RemoveFromRolesAsync(user, roles);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userService.FindByNameAsync(userName);
        }

        public async Task<SignInResult> PasswordSignInAsync(string username, string password)
        {
            return await _signInService.PasswordSignInAsync(username, password,false,false);
        }

        public async Task SignOutAsync()
        {
            await _signInService.SignOutAsync();
           
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userService.GeneratePasswordResetTokenAsync(user);
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return _userService.GetUserId(principal);
        }

        public void UpdateUserLectures(User entity, int[] LectureIds, EnumApprovalStatus approvalStatus)
        {
            _userDal.UpdateUserLectures(entity, LectureIds,approvalStatus);
        }

        public User GetThatUsersLectures(int id)
        {
            return _userDal.GetThatUsersLectures(id);
        }

        public User GetThatAcademiciansStudents(int id)
        {
            return _userDal.GetThatAcademiciansStudents(id);
        }
    }
}

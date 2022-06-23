using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelAppAutomation.Controllers
{
   // [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;
        public UserController(IUserService userService, IRoleService roleService, IDepartmentService departmentService)
        {
            _userService = userService;
            _roleService = roleService;
            _departmentService = departmentService;
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult Add()

        {
            var model = new RegisterModel()
            {
                Roles = _roleService.List(),
                Departments = _departmentService.List()
            };

            return View(model);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(RegisterModel model, int[] RoleIds, int[] DepartmentIds)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname

            };
            var result = await _userService.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                foreach (var item in RoleIds)
                {
                    Role applicationRole = await _roleService.FindByIdAsync(item.ToString());
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userService.AddToRoleAsync(user, applicationRole.Name);
                    }
                }
                _userService.UpdateUserDepartments(user, DepartmentIds);
                var departmentslectures = new List<Lecture>();
                foreach (var item in DepartmentIds)
                {
                    
                     departmentslectures.AddRange(_departmentService.GetThatDepartmentsLectures(item).Lectures.ToList());
                    
                 
                }
                departmentslectures.Count();

                var LectureIds = (departmentslectures.Select(i => i.Id).ToList());

                if (LectureIds.Count!=0)
                {
                    _userService.UpdateUserLectures(user, LectureIds.ToArray(), EnumApprovalStatus.NotSubmitted);
                }
                   
            
                


                return RedirectToAction("List");
            }

            ModelState.AddModelError("", "Bir hata oluştu lütfen tekrar deneyiniz.");

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> List(string UserId)
        {

            var user = await _userService.FindByIdAsync(UserId);

            var model = new UserViewModel()
            {
                Users = _userService.List(),
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return NotFound();
            };
            var user = await _userService.FindByIdAsync(id.ToString());
            var entity = _userService.GetThatUsersDepartments((int)id);

            if (entity == null)
            {
                ViewBag.ErrorMessage = $"Id={id} li kullanıcı bulunamadı";
                return NotFound();
            }

            var model = new UserUpdateModel()
            {

                UserId = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                UserName = entity.UserName,
                Email = entity.Email,
                SelectedDepartments = entity.UserDepartments.Select(i => i.Department).ToList(),
                Roles = _roleService.List(),
                SelectedRoles = await _userService.GetRolesAsync(user),


            };

            //ViewBag.Roles = _roleService.List();
            ViewBag.Departments = _departmentService.List();
            ViewBag.Roles = _roleService.List();

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateModel model, int[] RoleIds, int[] DepartmentIds)
        {
            if (ModelState.IsValid)
            {
                var entity = await _userService.FindByIdAsync(model.UserId.ToString());
                if (entity == null)
                {
                    return NotFound();
                }
                entity.UserName = model.UserName;
                entity.Name = model.Name;
                entity.Surname = model.Surname;
                entity.Email = model.Email;

                var roles = await _userService.GetRolesAsync(entity);

                await _userService.UpdateAsync(entity);

                var token = await _userService.GeneratePasswordResetTokenAsync(entity);
                 await _userService.ResetPasswordAsync(entity, token, model.NewPassword);
                //await _userService.ChangePassword(entity, model.Password, model.NewPassword);
                await _userService.RemoveFromRolesAsync(entity, roles);
                foreach (var item in RoleIds)
                {
                    var applicationRole = await _roleService.FindByIdAsync(item.ToString());
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userService.AddToRoleAsync(entity, applicationRole.Name);
                        
                    }
                }
                
                _userService.UpdateUserDepartments(entity,DepartmentIds);
                
                return RedirectToAction("List");
            }

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int UserId)
        {
            var entity = await _userService.FindByIdAsync(UserId.ToString());

            if (entity != null)
            {
                await _userService.Delete(entity);
            }

            return RedirectToAction("List");
        }
        public IActionResult Login()
            
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string returnUrl)
        {
            returnUrl = returnUrl ?? "~/";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userService.FindByNameAsync(model.UserName);
            if (user==null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı bulunmamaktadır.Kullanıcı adınızı doğru yazdığınızdan emin olun.");
            }
            var result = await _userService.PasswordSignInAsync(model.UserName, model.Password);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToAction("Login","User");
        }


    }
}

using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArelAppAutomation.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(Role role)
        {
            await _roleService.Add(role);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult List()
        {
            var model = new RoleViewModel()
            {
                Roles = _roleService.List()
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {

            if (id == null)
            {
                return NotFound();
            };

            var role = await _roleService.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Id={id} li rol bulunamadı";
                return NotFound();
            }

            var model = new RoleViewModel()
            {
                Name = role.Name,
                RoleId = role.Id

            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            var entity = await _roleService.FindByIdAsync(model.RoleId.ToString());

            entity.Name = model.Name;

            await _roleService.Update(entity);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int RoleId)
        {
            var entity = await _roleService.FindByIdAsync(RoleId.ToString());

            if (entity != null)
            {
                await _roleService.Delete(entity);
            }

            return RedirectToAction("List");
        }


    }
}

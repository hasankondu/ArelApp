using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ArelApp.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult List()
        {
            var model = new DepartmentViewModel()
            {
                Departments = _departmentService.List()
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Department department)
        {
            _departmentService.Add(department);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            };

            var entity = _departmentService.GetById((int)id);



            var model = new DepartmentViewModel()
            {
                DepartmentId = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Departments = _departmentService.List()

            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(DepartmentViewModel model)
        {
            var entity = _departmentService.GetById(model.DepartmentId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Code = model.Code;

            _departmentService.Update(entity);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int DepartmentId)
        {
            var entity = _departmentService.GetById(DepartmentId);

            if (entity != null)
            {
                _departmentService.Delete(entity);
            }

            return RedirectToAction("List");
        }
    }
}

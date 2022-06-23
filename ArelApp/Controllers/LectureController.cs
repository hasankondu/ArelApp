using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelAppAutomation.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;
        private readonly IDepartmentService _departmentService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public LectureController(ILectureService lectureService, IDepartmentService departmentService, IUserService userService, IRoleService roleService)
        {
            _lectureService = lectureService;
            _departmentService = departmentService;
            _userService = userService;
            _roleService = roleService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult List()
        {
            var model = new LectureViewModel()
            {
                Lectures = _lectureService.List()

            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var allacademicians = await _userService.GetUsersInRoleAsync("Academician");
            List<User> academicians = new List<User>();
            academicians.AddRange(allacademicians);

            var model = new LectureViewModel()
            {
                Departments = _departmentService.List(),
                Users = academicians,
            };

            ViewBag.Departments = new SelectList(_departmentService.List(), "Id", "Name");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(LectureViewModel model)
        {
            var entity = new Lecture()
            {
                Name = model.Name,
                Credit = model.Credit,
                Code = model.Code,
                DepartmentId = model.DepartmentId
            };


            _lectureService.Add(entity);
            var lecture = _lectureService.GetById(entity.Id);
            var user = await _userService.FindByIdAsync(model.UserId.ToString());
            _userService.AssignUserToLecture(user, lecture.Id, EnumApprovalStatus.Teaching);
            var studentsinrole = await _userService.GetUsersInRoleAsync("Student");
            var students = new List<User>();
            var departments = _departmentService.List();

            foreach (var item in studentsinrole)
            {
                students.AddRange(_userService.GetThatUsersDepartments(item.Id).UserDepartments.Where(i => i.DepartmentId == model.DepartmentId).Select(i=>i.User).ToList());
            }
        
            
            
            foreach (var student in students)
            {
                _userService.AssignUserToLecture(student, lecture.Id, EnumApprovalStatus.NotSubmitted);
            }

            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            };

            var entity = _lectureService.GetById((int)id);

            //var allacademicians = await _userService.GetUsersInRoleAsync("Academician");
            List<User> academicians = new List<User>();
            var academician = _lectureService.GetThatLecturesAcademician(entity.Id).UserLectures.Where(i => i.ApprovalStatus == EnumApprovalStatus.Teaching).Select(i=>i.User).FirstOrDefault();
            var usersindepartment = _userService.GetThatUsersByDepartmentId(entity.DepartmentId);
            foreach (var user in usersindepartment)
            {
                var inrole = await _userService.IsInRoleAsync(user, "Academician");
                if (inrole == true)
                {
                    academicians.Add(user);
                }
            }
          

            var departments = _departmentService.List();
            var model = new LectureViewModel()
            {
                LectureId = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Credit = entity.Credit,
                DepartmentId = entity.DepartmentId,
                Departments = departments,
                Users = academicians,
                UserId = academician.Id

            };


            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            ViewBag.Academicians = new SelectList(academicians, "Id", "Name");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(LectureViewModel model)
        {
            var entity = _lectureService.GetById(model.LectureId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.Credit = model.Credit;
            entity.DepartmentId = model.DepartmentId;

            var academician = _lectureService.GetThatLecturesAcademician(entity.Id).UserLectures.Where(i => i.ApprovalStatus == EnumApprovalStatus.Teaching).Select(i => i.User).FirstOrDefault();

            _lectureService.Update(entity);
            _userService.ReAssignAcademicianToLecture(academician, model.UserId,entity.Id, EnumApprovalStatus.Teaching);
         
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int LectureId)
        {
            var entity = _lectureService.GetById(LectureId);

            if (entity != null)
            {
                _lectureService.Delete(entity);
            }

            return RedirectToAction("List");
        }



        //burayi tekrar yap
        [HttpGet]
        public IActionResult Choose()
        {
            var userid = _userService.GetUserId(User);
            var departments = (_userService.GetThatUsersDepartments(int.Parse(userid))).UserDepartments.Select(i => i.Department).ToList();
            var model = new ChoiceViewModel();

            foreach (var department in departments)
            {


                var lectures = new List<Lecture>();


                lectures.AddRange(_userService.GetThatUsersLectures(int.Parse(userid)).UserLectures.Where(i => i.ApprovalStatus == (EnumApprovalStatus.NotSubmitted)||i.ApprovalStatus==(EnumApprovalStatus.Denied)).Select(i => i.Lecture).Where(i => i.DepartmentId == department.Id).ToList());
                var departmentandtheirlectures = new LectureChoiceViewModel();
                departmentandtheirlectures.DepartmentName = department.Name;
                departmentandtheirlectures.Lectures = lectures;


                model.LectureswithDepartment.Add(departmentandtheirlectures);


            }



            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> Choose(int[] LectureIds)
        {
            var userid = _userService.GetUserId(User);
            var user = await _userService.FindByIdAsync(userid);
            _userService.UpdateUserLectures(user, LectureIds, EnumApprovalStatus.Pending);

            return RedirectToAction("MyLectures");
        }



        [HttpGet]
        public IActionResult Approve()
        {
            var academicianid = _userService.GetUserId(User);
            var departments = (_userService.GetThatUsersDepartments(int.Parse(academicianid))).UserDepartments.Select(i => i.Department).ToList();
            var allstudentsinsamedepartment = new List<User>();

            var model = new ApproveModel();


            foreach (var department in departments)
            {
                var studentmodel = new StudentApproveModel();
                studentmodel.DepartmentName = department.Name;
                allstudentsinsamedepartment.AddRange(_departmentService.GetThatDepartmentsUsers(department.Id).UserDepartments.Where(i => i.UserId != int.Parse(academicianid)).Select(i => i.User).ToList());
                var studentsinsamedepartment = allstudentsinsamedepartment.GroupBy(x => x.Id).Select(g => g.FirstOrDefault()).ToList();
                foreach (var student in studentsinsamedepartment)
                {
                    var lectures = new List<Lecture>();
                    lectures.AddRange(_userService.GetThatUsersLectures(student.Id).UserLectures.Where(i => i.ApprovalStatus == EnumApprovalStatus.Pending).Select(i => i.Lecture).Where(i => i.DepartmentId == department.Id).ToList());

                    if (lectures.Count != 0)
                    {
                        var lecturemodel = new LectureApproveModel();
                        lecturemodel.Lectures = lectures;
                        lecturemodel.StudentName = $"{student.Name} {student.Surname}";
                        lecturemodel.StudentId = student.Id;
                        studentmodel.StudentsLectures.Add(lecturemodel);
                    }

                }
                model.StudentswithLectures.Add(studentmodel);
            }


            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Approve(int StudentId, int[] LectureIds)
        {
            var user = await _userService.FindByIdAsync(StudentId.ToString());
            _userService.UpdateUserLectures(user, LectureIds, EnumApprovalStatus.Approved);

            return RedirectToAction("Approve");
        }
        [HttpPost]
        public async Task<IActionResult> Deny(int StudentId, int[] LectureIds)
        {

            var user = await _userService.FindByIdAsync(StudentId.ToString());
            _userService.UpdateUserLectures(user, LectureIds, EnumApprovalStatus.Denied);

            return RedirectToAction("Approve");
        }
        //departmana göre de listele
        //kayıt olurken eğer akademisyense derslerin durumu teaching vb bir şey olsun


        [HttpGet]
        public IActionResult MyLectures()
        {
            var userid = _userService.GetUserId(User);
            var departments = (_userService.GetThatUsersDepartments(int.Parse(userid))).UserDepartments.Select(i => i.Department).ToList();
            var model = new ChoiceViewModel();

            foreach (var department in departments)
            {


                var lectures = new List<Lecture>();


                lectures.AddRange(_userService.GetThatUsersLectures(int.Parse(userid)).UserLectures.Where(i => i.ApprovalStatus == (EnumApprovalStatus.Approved)).Select(i => i.Lecture).Where(i => i.DepartmentId == department.Id).ToList());
                var departmentandtheirlectures = new LectureChoiceViewModel();
                departmentandtheirlectures.DepartmentName = department.Name;
                departmentandtheirlectures.Lectures = lectures;


                model.LectureswithDepartment.Add(departmentandtheirlectures);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult MyTeachings()
        {
            var userid = _userService.GetUserId(User);
            var departments = (_userService.GetThatUsersDepartments(int.Parse(userid))).UserDepartments.Select(i => i.Department).ToList();
            var model = new ChoiceViewModel();

            foreach (var department in departments)
            {


                var lectures = new List<Lecture>();


                lectures.AddRange(_userService.GetThatUsersLectures(int.Parse(userid)).UserLectures.Where(i => i.ApprovalStatus == (EnumApprovalStatus.Teaching)).Select(i => i.Lecture).Where(i => i.DepartmentId == department.Id).ToList());
                var departmentandtheirlectures = new LectureChoiceViewModel();
                departmentandtheirlectures.DepartmentName = department.Name;
                departmentandtheirlectures.Lectures = lectures;


                model.LectureswithDepartment.Add(departmentandtheirlectures);
            }
            return View(model);
        }

        public async Task<JsonResult> LoadAcademicians(int Id)
        {
            var academician = new List<User>();
             var asd=_userService.GetThatUsersByDepartmentId(Id);
            foreach (var user in asd)
            {
                var inrole = await _userService.IsInRoleAsync(user, "Academician");
                if (inrole==true)
                {
                    academician.Add(user);
                }
            }

            
            return Json(new SelectList(academician, "Id", "Name"));
        }



    }
}

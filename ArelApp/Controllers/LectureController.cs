using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ArelApp.Entities.Concrete.UserLecture;

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
        public IActionResult Add()
        {
            var model = new LectureViewModel()
            {
                Departments = _departmentService.List()
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(LectureViewModel model)
        {
            var entity = new Lecture()
            {
                Name = model.Name,
                Credit = model.Credit,
                Code = model.Code,
                DepartmentId = model.DepartmentId
            };
            _lectureService.Add(entity);
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

            var entity = _lectureService.GetById((int)id);



            var model = new LectureViewModel()
            {
                LectureId = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Credit = entity.Credit,
                DepartmentId = entity.DepartmentId,
                Departments = _departmentService.List()

            };

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

            _lectureService.Update(entity);
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

        [HttpGet]
        public IActionResult Result()
        {
            var lecture = _lectureService.GetByStudentId(_userService.GetUserId(User));


            return View(new LectureModel()
            {
                LectureId = lecture.Id,
                Exams = lecture.Exams.Select(i => new ExamModel()
                {
                    ExamId = i.Id,
                    Midterm = i.Midterm,
                    Final = i.Final
                }).ToList()
            });

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
        public async Task<IActionResult> Approve()
        {
            var academicianid = _userService.GetUserId(User);
            var academician = await _userService.FindByIdAsync(academicianid);
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
            _userService.UpdateUserLectures(user, LectureIds, EnumApprovalStatus.Pending);

            return View();
        }
        //departmana göre de listele

        [HttpGet]
        public async Task<IActionResult> MyLectures()
        {
            //var userid = _userService.GetUserId(User);
            //var user = await _userService.FindByIdAsync(userid);
            //var roles = await _userService.GetRolesAsync(user);
            //var departments = _userService.GetThatUsersDepartments(user.Id).UserDepartments.Select(i => i.Department).ToList();
            //var entity = _userService.GetThatUsersLectures(user.Id);
            //var model = new MyLecturesModel();

            //foreach (var department in departments)
            //{

            //    var lecture = new MyLectureViewModel();

            //    lecture.Lectures = entity.UserLectures.Where(i => i.ApprovalStatus == EnumApprovalStatus.Approved).Select(i => i.Lecture).ToList();
            //    lecture.DepartmentName = department.Name;
            //    lecture.Relation = "Öğrenimlerim";
            //    model.LectureswithDepartment.Add(lecture);

            //}

            //return View(model);
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

    }
}

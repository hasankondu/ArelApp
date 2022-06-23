//using ArelApp.Business.Abstract;
using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelAppAutomation.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly ILectureService _lectureService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        public ExamController(IExamService examService, ILectureService lectureService, IUserService userService, IDepartmentService departmentService)
        {
            _examService = examService;
            _lectureService = lectureService;
            _userService = userService;
            _departmentService = departmentService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult List()
        {
            var model = new ExamViewModel()
            {
                Exams = _examService.List()
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new ExamViewModel()
            {
                Lectures = _lectureService.List()
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(ExamViewModel model)
        {
            var entity = new Exam()
            {
                Midterm = model.Midterm,
                Final = model.Final,
                LectureId = model.LectureId
            };
            _examService.Add(entity);
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

            var entity = _examService.GetById((int)id);



            var model = new ExamViewModel()
            {
                ExamId = entity.Id,
                Midterm = entity.Midterm,
                Final = entity.Final,
                LectureId = entity.LectureId,
                Lectures = _lectureService.List()

            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(ExamViewModel model)
        {
            var entity = _examService.GetById(model.ExamId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Midterm = model.Midterm;
            entity.Final = model.Final;
            entity.LectureId = model.LectureId;

            _examService.Update(entity);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int ExamId)
        {
            var entity = _examService.GetById(ExamId);

            if (entity != null)
            {
                _examService.Delete(entity);
            }

            return RedirectToAction("List");
        }
        [Authorize(Roles = "Student")]
        public IActionResult Result()
        {
            var studentid = _userService.GetUserId(User);
            var departments = _userService.GetThatUsersDepartments(int.Parse(studentid)).UserDepartments.Select(i => i.Department).Where(i => i.Lectures.Any(i => i.UserLectures.Any(i => i.ApprovalStatus == EnumApprovalStatus.Approved))).ToList();
            var model = new ResultModel();

            foreach (var department in departments)
            {
                var resultdepartmentmodel = new ResultDepartmentModel();
                resultdepartmentmodel.DepartmentName = department.Name;
                var lecturesindepartment = new List<Lecture>();
                lecturesindepartment.AddRange(_departmentService.GetThatDepartmentsLectures(department.Id).Lectures.Where(i=>i.UserLectures.Any(i=>i.ApprovalStatus==EnumApprovalStatus.Approved)).Where(i=>i.UserLectures.Any(i=>i.UserId==int.Parse(studentid))).ToList());

                foreach (var lecture in lecturesindepartment)
                {
                    var resultlecturemodel = new ResultLectureModel();
                    resultlecturemodel.LectureName = lecture.Name;
                    
                    var exam = _examService.GetByStudentIdandLectureId(studentid, lecture.Id);
                    if (exam!=null)
                    {
                        resultlecturemodel.Midterm = exam.Midterm.ToString();
                        resultlecturemodel.Final = exam.Final.ToString();
                    }
                   

                    resultdepartmentmodel.Lectures.Add(resultlecturemodel);

                }
                model.DepartmentwithLectures.Add(resultdepartmentmodel);
            }

            return View(model);
        }
        [Authorize(Roles = "Academician")]
        [HttpGet]
        public async Task<IActionResult> Grade()
        {
            var academicianid = _userService.GetUserId(User);
            //var departments = (_userService.GetThatUsersDepartments(int.Parse(academicianid))).UserDepartments.Select(i => i.Department).ToList();
            var departments = _userService.GetThatUsersDepartments(int.Parse(academicianid)).UserDepartments.Select(i => i.Department).Where(i => i.Lectures.Any(i => i.UserLectures.Any(i => i.ApprovalStatus == EnumApprovalStatus.Teaching))).ToList();
            var academicianlectures = _userService.GetThatUsersLectures(int.Parse(academicianid)).UserLectures.Where(i => i.ApprovalStatus == EnumApprovalStatus.Teaching).Select(i => i.Lecture).ToList();
            var studentsinrole = await _userService.GetUsersInRoleAsync("Student");
            
            var model = new GradeViewModel();

            foreach (var department in departments)
            {
                var grademodel = new GradeModel();
                grademodel.DepartmentName = department.Name;
                var lecturesindepartment = new List<Lecture>();
                
                lecturesindepartment.AddRange(_departmentService.GetThatDepartmentsLectures(department.Id).Lectures.Where(i => i.UserLectures.Any(i => i.ApprovalStatus == EnumApprovalStatus.Teaching)).Where(i => i.UserLectures.Any(i => i.UserId == int.Parse(academicianid))).ToList());
               

                foreach (var lecture in lecturesindepartment)
                {

                    var studentviewmodel = new StudentViewModel();
                    studentviewmodel.LectureId = lecture.Id;
                    studentviewmodel.LectureName = lecture.Name;
                    var studentsinlecture = _lectureService.GetThatLecturesStudents(lecture.Id).UserLectures.Where(i => i.ApprovalStatus == EnumApprovalStatus.Approved).Where(i => i.LectureId == lecture.Id).Select(i => i.User).ToList();

                    List<int> studentIds = new List<int>();
                    if (studentsinlecture.Count != 0)
                    {

                        
                        foreach (var student in studentsinlecture)
                        {
                            var studentmodel = new StudentModel();
                            studentmodel.Id = student.Id;
                            studentmodel.Name = student.Name;
                            studentmodel.Surname = student.Surname;

                            var exam = _examService.GetByStudentIdandLectureId(student.Id.ToString(), lecture.Id);

                            if (exam != null)
                            {

                                studentmodel.ExamId = exam.Id.ToString();
                                studentmodel.Midterm = exam.Midterm;
                                studentmodel.Final = exam.Final;

                            }
                            else
                            {
                                studentmodel.Midterm = 0;
                                studentmodel.Final = 0;
                                studentIds.Add(student.Id);
                            }

                            studentviewmodel.Students.Add(studentmodel);

                        }
                       
                    }
                    _lectureService.CreateExam(studentIds.ToArray(), lecture.Id);

                    grademodel.LecturesStudents.Add(studentviewmodel);

                }
                model.LectureswithDepartment.Add(grademodel);
            }


            return View(model);
        }
        [Authorize(Roles = "Academician")]
        [HttpPost]
        public IActionResult Grade(int[] StudentIds, int LectureId, int[] Midterms, int[] Finals)
        {

            _lectureService.UpdateStudentExams(LectureId, StudentIds, Midterms, Finals);


            return RedirectToAction("Grade");
        }
    }
}

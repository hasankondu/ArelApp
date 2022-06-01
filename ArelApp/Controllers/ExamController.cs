using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ArelAppAutomation.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly ILectureService _lectureService;
        private readonly IUserService _userService;
        public ExamController(IExamService examService, ILectureService lectureService, IUserService userService)
        {
            _examService = examService;
            _lectureService = lectureService;
            _userService = userService;
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
                AcademicianId = entity.AcademicianId,
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

        public IActionResult Result()
        {
            var exam = _examService.GetByStudentId(_userService.GetUserId(User));

            //return View(new StudentLectureModel()
            //{
            //    LectureId=lecture.Id,
            //    Results=lecture.Select(i=> new(StudentResultModel)
            //    { })
            //});
            return View();
        }
    }
}

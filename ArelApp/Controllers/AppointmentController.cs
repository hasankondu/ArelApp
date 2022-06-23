using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using ArelApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILectureService _lectureService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        public AppointmentController(IAppointmentService appointmentService, ILectureService lectureService, IUserService userService, IDepartmentService departmentService)
        {
            _appointmentService = appointmentService;
            _lectureService = lectureService;
            _userService = userService;
            _departmentService = departmentService;
        }
        [Authorize(Roles = "Academician")]
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
        [Authorize(Roles = "Academician")]
        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            var academicianid = _userService.GetUserId(User);
            appointment.AcademicianId = academicianid;
            appointment.Status = EnumAppointmentStatus.NotSubmitted;
            _appointmentService.Add(appointment);
            
            
            return View();
        }
        [Authorize(Roles = "Student")]
        [HttpGet]
        public IActionResult Book()
        {
            var studentid = _userService.GetUserId(User);
            var studentslectures = _userService.GetThatUsersLectures(int.Parse(studentid)).UserLectures.Where(i=>i.ApprovalStatus==EnumApprovalStatus.Approved).Select(i=>i.Lecture).ToList();
            var academicians = new List<User>();
            foreach (var item in studentslectures)
            {
               academicians.AddRange(_lectureService.GetThatLecturesAcademician(item.Id).UserLectures.Where(i=>i.ApprovalStatus==EnumApprovalStatus.Teaching).Select(i=>i.User).ToList());
            }
            academicians = academicians.GroupBy(i => i.Id).Select(i=>i.FirstOrDefault()).ToList();
            
            var appointments = new List<Appointment>();
            foreach (var academician in academicians)
            {
                appointments.AddRange(_appointmentService.GetByAcademicianId(academician.Id.ToString()));
            }

            var model = new AppointmentViewModel()
            {
                
                Academicians = academicians
            };



            ViewBag.Academicians = new SelectList(academicians, "Id", "Name");
            
            return View(model);
        }
        [Authorize(Roles = "Student")]
        [HttpPost]
        public IActionResult Book(AppointmentViewModel model)
        {
            var studentid = _userService.GetUserId(User);
            var entity = _appointmentService.GetById(model.AppointmentId);

            entity.StudentId = studentid;
            entity.Status = EnumAppointmentStatus.Pending;
            entity.Subject = model.Subject;
            entity.Comment = model.Comment;
            entity.AcademicianId = model.AcademicianId;

            _appointmentService.Update(entity);

            return RedirectToAction("MyAppointments");
        }
        [Authorize(Roles = "Academician")]
        [HttpGet]
        public async Task<IActionResult> Approve()
        {
            var academicianid = _userService.GetUserId(User);
            var appointments = _appointmentService.GetByAcademicianId(academicianid).Where(i=>i.Status==EnumAppointmentStatus.Pending).ToList();
            var appointmentmodel = new AppointmentModel();
            var model = new AppointmentListModel();
            foreach (var item in appointments)
            {
                appointmentmodel.AcademicianId = item.AcademicianId;
                appointmentmodel.Comment = item.Comment;
                appointmentmodel.Date = item.Date;
                appointmentmodel.Subject = item.Subject;
                appointmentmodel.StudentId = item.StudentId;
                appointmentmodel.AppointmentId = item.Id;
                var student = await _userService.FindByIdAsync(item.StudentId);
                appointmentmodel.StudentName = student.Name + " " + student.Surname;
                model.Appointments.Add(appointmentmodel);
            }
            
           
            return View(model);
        }
        [Authorize(Roles = "Academician")]
        [HttpPost]
        public IActionResult Approve(int AppointmentId)
        {
          
            var appointment = _appointmentService.GetById(AppointmentId);
            appointment.Status = EnumAppointmentStatus.Approved;
            _appointmentService.Update(appointment);
            return RedirectToAction("Index","Home");
        }
        [Authorize(Roles = "Academician")]
        [HttpPost]
        public IActionResult Reject(int AppointmentId)
        {
           
            var appointment = _appointmentService.GetById(AppointmentId);
            appointment.Status = EnumAppointmentStatus.Pending;
            appointment.StudentId = "";
            appointment.Subject = "";
            appointment.Comment = "";
            _appointmentService.Update(appointment);
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            var studentid = _userService.GetUserId(User);
            var appointments = _appointmentService.GetByStudentId(studentid).Where(i=>i.Status==EnumAppointmentStatus.Approved).ToList();
            var model = new AppointmentListModel();
            foreach (var appointment in appointments)
            {
                var appointmentmodel = new AppointmentModel();
                appointmentmodel.Date = appointment.Date;
                var academician = await _userService.FindByIdAsync(appointment.AcademicianId);
                appointmentmodel.AcademicianName = academician.Name + " " + academician.Surname;
                appointmentmodel.Subject = appointment.Subject;
                model.Appointments.Add(appointmentmodel);
                
            }
            return View(model);
        }
        [Authorize(Roles = "Academician")]
        [HttpGet]
        public async Task<IActionResult> MyMeetings()
        {
            var academicianid = _userService.GetUserId(User);
            var appointments = _appointmentService.GetByAcademicianId(academicianid).Where(i => i.Status == EnumAppointmentStatus.Approved).ToList();
            var model = new AppointmentListModel();
            foreach (var appointment in appointments)
            {
                var appointmentmodel = new AppointmentModel();
                appointmentmodel.Date = appointment.Date;
                var student = await _userService.FindByIdAsync(appointment.StudentId);
                appointmentmodel.StudentId = student.Id.ToString();
                appointmentmodel.StudentName = student.Name + " " + student.Surname;
                appointmentmodel.Subject = appointment.Subject;
                model.Appointments.Add(appointmentmodel);

            }
            return View(model);
        }


        public JsonResult LoadAppointments(int Id)
        {
            var appointment = new List<Appointment>();
            var entity = _appointmentService.GetByAcademicianId(Id.ToString());
            foreach (var item in entity)
            {
                
                if (item.AcademicianId==Id.ToString())
                {
                    appointment.Add(item);
                }
            }

            return Json(new SelectList(appointment, "Id", "Date"));
        }
    }
}

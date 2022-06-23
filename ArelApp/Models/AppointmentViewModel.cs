using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class AppointmentViewModel
    {
        public AppointmentViewModel()
        {
            Appointments = new List<Appointment>();
            Academicians = new List<User>();
        }

        public int AppointmentId { get; set; }
        public string Subject { get; set; }
        [StringLength(10000,ErrorMessage ="Maksimum 10000 karakter kullanabilirsiniz")]
        public string Comment { get; set; }
        public string Date { get; set; }
        public string AcademicianId { get; set; }
        public string StudentId { get; set; }
        public EnumAppointmentStatus Status { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<User> Academicians { get; set; }
    }
}

using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public string AcademicianId { get; set; }
        public string AcademicianName { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public EnumAppointmentStatus Status { get; set; }
    }

    public class AppointmentListModel
    {
        public AppointmentListModel()
        {
            Appointments = new List<AppointmentModel>();
        }
        public List<AppointmentModel> Appointments { get; set; }
    }
}

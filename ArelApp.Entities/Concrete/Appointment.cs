using ArelApp.Core.Entities;


namespace ArelApp.Entities.Concrete
{
    public class Appointment : EntityBase, IEntity
    {
        public string Subject { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public string AcademicianId { get; set; }
        public string StudentId { get; set; }
        public EnumAppointmentStatus Status { get; set; }


    }

    public enum EnumAppointmentStatus
    {
        Approved = 0,
        Denied = 1,
        Pending = 2,
        NotSubmitted = 3

    }
}

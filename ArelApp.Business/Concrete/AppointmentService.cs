using ArelApp.Business.Abstract;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentDal _appointmentDal;

        public AppointmentService(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public void Add(Appointment appointment)
        {
            _appointmentDal.Add(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _appointmentDal.Delete(appointment);
        }

        public List<Appointment> GetByAcademicianId(string academicianid)
        {
            return _appointmentDal.GetByAcademicianId(academicianid);
        }

        public Appointment GetById(int id)
        {
            return _appointmentDal.GetById(id);
        }

        public List<Appointment> GetByStudentId(string studentid)
        {
            return _appointmentDal.GetByStudentId(studentid);
        }

        public void Update(Appointment appointment)
        {
            _appointmentDal.Update(appointment);
        }
    }
}

using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.Business.Abstract
{
    public interface IAppointmentService
    {
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
        Appointment GetById(int id);
        List<Appointment> GetByAcademicianId(string academicianid);
        List<Appointment> GetByStudentId(string studentid);
    }
}

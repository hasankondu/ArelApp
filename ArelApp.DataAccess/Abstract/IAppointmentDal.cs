using ArelApp.Core.DataAccess;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.DataAccess.Abstract
{
    public interface IAppointmentDal : IEntityRepository<Appointment>
    {
        List<Appointment> GetByAcademicianId(string academicianid);
        List<Appointment> GetByStudentId(string studentid);
    }
}

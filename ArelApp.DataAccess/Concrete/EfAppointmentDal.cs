using ArelApp.Core.DataAccess.EntityFramework;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace ArelApp.DataAccess.Concrete
{
    public class EfAppointmentDal : EfEntityRepositoryBase<Appointment, ArelAppAutomationContext>, IAppointmentDal
    {
        public List<Appointment> GetByAcademicianId(string academicianid)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Appointments
                    .Where(i=>i.AcademicianId== academicianid)
                    .ToList();
            }
        }

        public List<Appointment> GetByStudentId(string studentid)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Appointments
                    .Where(i => i.StudentId == studentid)
                    .ToList();
            }
        }
    }
}

using ArelApp.Core.DataAccess.EntityFramework;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace ArelApp.DataAccess.Concrete
{
    public class EfLectureDal : EfEntityRepositoryBase<Lecture, ArelAppAutomationContext>, ILectureDal
    {
        public Lecture GetByStudentId(string studentId)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Lectures
                    .Include(i => i.Exams)
                    .FirstOrDefault(i=>i.StudentId==studentId);
            }
        }


        public List<Lecture> List()
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Lectures
                    .Include(i => i.Department)
                    .ToList();
            }
        }

        public List<Lecture> GetUsersLecturesByDepartment(Lecture lecture)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Lectures
                        .Include(i=>i.Department)
                        .Where(i=>i.DepartmentId==lecture.DepartmentId)
                        .ToList();


            }

        }

    }
}

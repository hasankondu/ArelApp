using ArelApp.Core.DataAccess.EntityFramework;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace ArelApp.DataAccess.Concrete
{
    public class EfExamDal : EfEntityRepositoryBase<Exam, ArelAppAutomationContext>, IExamDal
    {
        public Exam GetByStudentId(string studentId)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context
                    .Exams
                    .FirstOrDefault(i => i.StudentId == studentId);
            }
        }
        public Exam GetByStudentIdandLectureId(string studentId,int lectureId)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context
                    .Exams
                    .Where(i=>i.LectureId==lectureId)
                    .FirstOrDefault(i => i.StudentId == studentId);
            }
        }

        public List<Exam> List()
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Exams
                    .Include(i => i.Lecture)
                    .ToList();
            }
        }

        
    }


}

using ArelApp.Core.DataAccess;
using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArelApp.DataAccess.Abstract
{
    public interface IExamDal : IEntityRepository<Exam>
    {
        List<Exam> List();
        Exam GetByStudentId(string studentId);
        Exam GetByStudentIdandLectureId(string studentId, int lectureId);

       

    }
}

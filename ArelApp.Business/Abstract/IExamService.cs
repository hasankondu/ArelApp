using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.Business.Abstract
{
    public interface IExamService
    {
        void Add(Exam exam);
        void Update(Exam exam);
        void Delete(Exam exam);
        List<Exam> List();
        Exam GetById(int id);
        Exam GetByStudentId(string studentId);
    }
}

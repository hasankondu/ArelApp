using ArelApp.Business.Abstract;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;


namespace ArelApp.Business.Concrete
{
    public class ExamService : IExamService
    {
        private readonly IExamDal _examDal;

        public ExamService(IExamDal examDal)
        {
            _examDal = examDal;
        }

        public void Add(Exam exam)
        {
            _examDal.Add(exam);
        }

        public void Delete(Exam exam)
        {
            _examDal.Delete(exam);
        }

        public Exam GetById(int id)
        {
            return _examDal.GetById(id);
        }

        public Exam GetByStudentId(string studentId)
        {
            return _examDal.GetByStudentId(studentId);
        }

        public List<Exam> List()
        {
            return _examDal.List();
        }

        public void Update(Exam exam)
        {
            _examDal.Update(exam);
        }
    }
}

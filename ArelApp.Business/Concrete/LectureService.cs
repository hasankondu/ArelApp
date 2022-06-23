using ArelApp.Business.Abstract;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;


namespace ArelApp.Business.Concrete
{
    public class LectureService : ILectureService
    {
        private readonly ILectureDal _lectureDal;

        public LectureService(ILectureDal lectureDal)
        {
            _lectureDal = lectureDal;
        }
        public void Add(Lecture lecture)
        {
            _lectureDal.Add(lecture);
        }

        public void Delete(Lecture lecture)
        {
            _lectureDal.Delete(lecture);
        }

        public void Update(Lecture lecture)
        {
            _lectureDal.Update(lecture);
        }

        public Lecture GetById(int id)
        {
            return _lectureDal.GetById(id);
        }

        public List<Lecture> List()
        {
            return _lectureDal.List();
        }

        //public Lecture GetByStudentId(string studentId)
        //{
        //    return _lectureDal.GetByStudentId(studentId);
        //}

        public List<Lecture> GetUsersLecturesByDepartment(Lecture lecture)
        {
            return _lectureDal.GetUsersLecturesByDepartment(lecture);
        }

        public Lecture GetThatLecturesAcademician(int lectureid)
        {
            return _lectureDal.GetThatLecturesAcademician(lectureid);
        }

        public Lecture GetThatLecturesStudents(int lectureid)
        {
            return _lectureDal.GetThatLecturesStudents(lectureid);
        }

        public void UpdateStudentExams(int LectureId, int[] StudentIds, int[] Midterms, int[] Finals)
        {
            _lectureDal.UpdateStudentExams(LectureId, StudentIds, Midterms, Finals);
        }

        public void CreateExam(int[] StudentIds, int LectureId)
        {
            _lectureDal.CreateExam(StudentIds, LectureId);
        }
    }
}

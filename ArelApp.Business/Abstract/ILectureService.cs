using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.Business.Abstract
{
    public interface ILectureService
    {
        void Add(Lecture lecture);
        void Update(Lecture lecture);
        void Delete(Lecture lecture);
        List<Lecture> List();
        Lecture GetById(int id);
        //Lecture GetByStudentId(string studentId);

        List<Lecture> GetUsersLecturesByDepartment(Lecture lecture);
        Lecture GetThatLecturesAcademician(int lectureid);

        Lecture GetThatLecturesStudents(int lectureid);
        void UpdateStudentExams(int LectureId, int[] StudentIds, int[] Midterms, int[] Finals);
        public void CreateExam(int[] StudentIds, int LectureId);
    }
}

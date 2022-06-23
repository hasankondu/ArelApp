using ArelApp.Core.DataAccess;
using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArelApp.DataAccess.Abstract
{
    public interface ILectureDal : IEntityRepository<Lecture>
    {
        List<Lecture> List();
        //Lecture GetByStudentId(string studentId);
        List<Lecture> GetUsersLecturesByDepartment(Lecture lecture);
        Lecture GetThatLecturesAcademician(int lectureid);
        Lecture GetThatLecturesStudents(int lectureid);
        void UpdateStudentExams(int LectureId, int[] StudentIds, int[] Midterms, int[] Finals);
        public void CreateExam(int[] StudentIds, int LectureId);
    }
}

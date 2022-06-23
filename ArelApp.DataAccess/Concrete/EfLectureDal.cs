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
        //public Lecture GetByStudentId(string studentId)
        //{
        //    using (var context = new ArelAppAutomationContext())
        //    {
        //        return context.Lectures
        //            .Include(i => i.Exams)
        //            .FirstOrDefault(i=>i.StudentId==studentId);
        //    }
        //}




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
        public Lecture GetThatLecturesAcademician(int lectureid)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Lectures
                    .Where(i => i.Id == lectureid)
                    .Include(i => i.UserLectures)
                    .ThenInclude(i=>i.User)
                    .FirstOrDefault();
            }
        }
        public Lecture GetThatLecturesStudents(int lectureid)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Lectures
                    .Where(i => i.Id == lectureid)
                    .Include(i => i.UserLectures)
                    .ThenInclude(i => i.User)
                    .FirstOrDefault();
            }
        }



        public void UpdateStudentExams(int LectureId, int[] StudentIds,int[] Midterms,int[] Finals)
        {
            using (var context = new ArelAppAutomationContext())
            {
                var lecture = context.Lectures
                    .Include(i => i.Exams)
                    .FirstOrDefault(i => i.Id == LectureId);
                if (lecture != null)
                {
                   

                    
                        var counter = 0;
                        foreach (var item in StudentIds)
                        {
                            var selected = lecture.Exams.Where(i => i.StudentId == item.ToString()).Select(i => new Exam()
                            {
                                StudentId = item.ToString(),
                                LectureId = lecture.Id,
                                Midterm = Midterms[counter],
                                Final = Finals[counter]
                            }).ToList();
                            var notselected = lecture.Exams.Where(i => i.StudentId != item.ToString()).ToList();

                            List<Exam> exams = new List<Exam>();

                            exams.AddRange(selected);
                            exams.AddRange(notselected);

                            lecture.Exams = exams;
                            

                            context.SaveChanges();
                            counter++;
                        }




                }
            }

        }

        public void CreateExam(int[] StudentIds, int LectureId)
        {
            using (var context = new ArelAppAutomationContext())
            {
                var lecture = context.Lectures
                    .Include(i => i.Exams)
                    .FirstOrDefault(i => i.Id == LectureId);
                if (lecture != null)
                {


                    if (lecture.Exams.Count() == 0)
                    {
                        lecture.Exams = StudentIds.Select(i => new Exam()
                        {
                            StudentId = i.ToString(),
                            LectureId = lecture.Id,
                            Midterm = 0,
                            Final = 0
                        }).ToList();

                        context.SaveChanges();

                    }
                }
            }
        }

    }
}

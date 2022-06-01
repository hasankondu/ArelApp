using ArelApp.Core.DataAccess.EntityFramework;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static ArelApp.Entities.Concrete.UserLecture;

namespace ArelApp.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, ArelAppAutomationContext>, IUserDal
    {

        public User GetThatAcademiciansStudents(int id)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Users
                   .Where(i => i.Id == id)
                   .Include(i => i.UserLectures)
                   .ThenInclude(i => i.Lecture)
                   .FirstOrDefault();
            }
        }

        public User GetThatUsersDepartments(int id)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Users
                    .Where(i => i.Id == id)
                    .Include(i => i.UserDepartments)
                    .ThenInclude(i => i.Department)
                    .FirstOrDefault();
            }
        }

        public User GetThatUsersLectures(int id)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Users
                    .Where(i => i.Id == id)
                    .Include(i => i.UserDepartments)
                    .ThenInclude(i => i.Department)
                    .Include(i => i.UserLectures)
                    .ThenInclude(i => i.Lecture)
                    .FirstOrDefault();
            }
        }

        public void UpdateUserDepartments(User entity, int[] DepartmentIds)
        {
            using (var context = new ArelAppAutomationContext())
            {
                var user = context.Users
                    .Include(i => i.UserDepartments)
                    .FirstOrDefault(i => i.Id == entity.Id);
                if (user != null)
                {
                    user.Name = entity.Name;
                    user.Surname = entity.Surname;
                    user.Email = entity.Email;
                    user.UserName = entity.UserName;

                    user.UserDepartments = DepartmentIds.Select(i => new UserDepartment()
                    {
                        DepartmentId = i,
                        UserId = user.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }

        }

        public void UpdateUserLectures(User entity, int[] LectureIds, EnumApprovalStatus approvalStatus)
        {
            using (var context = new ArelAppAutomationContext())
            {
                var user = context.Users
                    .Include(i => i.UserLectures)
                    .FirstOrDefault(i => i.Id == entity.Id);
                if (user != null)
                {
                    user.Name = entity.Name;
                    user.Surname = entity.Surname;
                    user.Email = entity.Email;
                    user.UserName = entity.UserName;

                    if (user.UserLectures.Count() == 0)
                    {
                        user.UserLectures = LectureIds.Select(i => new UserLecture()
                        {
                            LectureId = i,
                            UserId = user.Id,
                            ApprovalStatus = approvalStatus
                        }).ToList();

                        context.SaveChanges();
                    }
                    else
                    {
                        foreach (var item in LectureIds)
                        {
                            var selected=user.UserLectures.Where(i => i.LectureId == item).Select(i => new UserLecture()
                            {
                                LectureId = item,
                                UserId = user.Id,
                                ApprovalStatus = approvalStatus
                            }).ToList();
                            var notselected = user.UserLectures.Where(i => i.LectureId != item).ToList();

                            List<UserLecture> userLectures = new List<UserLecture>();

                            userLectures.AddRange(selected);
                            userLectures.AddRange(notselected);

                            user.UserLectures = userLectures;



                            context.SaveChanges();
                        }


                    }






                }
            }

        }





    }
}

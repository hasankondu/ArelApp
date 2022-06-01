using ArelApp.Core.DataAccess.EntityFramework;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace ArelApp.DataAccess.Concrete
{
    public class EfDepartmentDal : EfEntityRepositoryBase<Department, ArelAppAutomationContext>, IDepartmentDal
    {
        public List<Department> List()
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Departments
                    .ToList();
            }
        }
        public Department GetThatDepartmentsLectures(int id)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Departments
                    .Where(i => i.Id == id)
                    .Include(i => i.Lectures)
                    .FirstOrDefault();
            }
        }

        public Department GetThatDepartmentsUsers(int id)
        {
            using (var context = new ArelAppAutomationContext())
            {
                return context.Departments
                    .Where(i => i.Id == id)
                    .Include(i => i.UserDepartments)
                    .ThenInclude(i=>i.User)
                    .FirstOrDefault();
            }
        }

    }
}

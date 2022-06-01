using ArelApp.Core.DataAccess;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.DataAccess.Abstract
{
    public interface IDepartmentDal : IEntityRepository<Department>
    {
        List<Department> List();
        Department GetThatDepartmentsLectures(int id);
        Department GetThatDepartmentsUsers(int id);
    }
}

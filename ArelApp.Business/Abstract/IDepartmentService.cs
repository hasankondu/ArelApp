using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.Business.Abstract
{
    public interface IDepartmentService
    {
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
        List<Department> List();
        Department GetById(int id);
        Department GetThatDepartmentsLectures(int id);

        Department GetThatDepartmentsUsers(int id);

    }
}

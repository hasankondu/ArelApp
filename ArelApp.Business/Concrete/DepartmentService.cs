using ArelApp.Business.Abstract;
using ArelApp.DataAccess.Abstract;
using ArelApp.Entities.Concrete;
using System.Collections.Generic;

namespace ArelApp.Business.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        public DepartmentService(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }
        public void Add(Department department)
        {
            _departmentDal.Add(department);
        }

        public void Delete(Department department)
        {
            _departmentDal.Delete(department);
        }

        public Department GetById(int id)
        {
            return _departmentDal.GetById(id);
        }

        public Department GetThatDepartmentsLectures(int id)
        {
            return _departmentDal.GetThatDepartmentsLectures(id);
        }

        public Department GetThatDepartmentsUsers(int id)
        {
            return _departmentDal.GetThatDepartmentsUsers(id);
        }

        public List<Department> List()
        {
            return _departmentDal.List();
        }

        public void Update(Department department)
        {
            _departmentDal.Update(department);
        }
    }
}

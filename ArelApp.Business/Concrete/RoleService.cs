using ArelApp.Business.Abstract;
using ArelApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArelApp.Business.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleDal;
        public RoleService(RoleManager<Role> roleDal)
        {
            _roleDal = roleDal;
        }
        public async Task Add(Role role)
        {
            await _roleDal.CreateAsync(role);
        }

        public async Task Delete(Role role)
        {
            await _roleDal.DeleteAsync(role);
        }

        public async Task<Role> FindByIdAsync(string id)
        {
            return await _roleDal.FindByIdAsync(id);
        }

        public List<Role> List()
        {
            return _roleDal.Roles.ToList();
        }

        public async Task Update(Role role)
        {
            await _roleDal.UpdateAsync(role);
            
        }
    }
}

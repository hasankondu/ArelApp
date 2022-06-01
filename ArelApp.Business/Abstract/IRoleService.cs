using ArelApp.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArelApp.Business.Abstract
{
    public interface IRoleService
    {
        Task Add(Role role);
        List<Role> List();
        Task<Role> FindByIdAsync(string id);

        Task Delete(Role role);
        Task Update(Role role);
    }
}

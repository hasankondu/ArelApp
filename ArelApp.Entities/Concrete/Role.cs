using ArelApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ArelApp.Entities.Concrete
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role() : base()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}

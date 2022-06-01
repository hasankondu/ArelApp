using ArelApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArelApp.Entities.Concrete
{
    public class UserRole : IdentityUserRole<int>, IEntity
    {
    }
}

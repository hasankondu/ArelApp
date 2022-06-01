using ArelApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArelAppAutomation.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }
    }
}

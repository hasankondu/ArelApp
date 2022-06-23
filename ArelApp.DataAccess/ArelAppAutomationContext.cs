using ArelApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArelApp.DataAccess
{
    public class ArelAppAutomationContext : IdentityDbContext<User, Role, int>
    {
        public ArelAppAutomationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ArelAutoDb;integrated security=true;");
        }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserDepartment>()
               .HasKey(x => new { x.DepartmentId, x.UserId });
            builder.Entity<UserLecture>()
               .HasKey(x => new { x.LectureId, x.UserId });
        }
    }
}
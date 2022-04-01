using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem_Web_API_Finale.Models;
using StudentManagementSystem_MVC_Finale.Models;

namespace StudentManagementSystem_MVC_Finale.Data
{
    public class StudentManagementSystem_MVC_FinaleContext : DbContext
    {
        public StudentManagementSystem_MVC_FinaleContext (DbContextOptions<StudentManagementSystem_MVC_FinaleContext> options)
            : base(options)
        {
        }

        public DbSet<StudentManagementSystem_Web_API_Finale.Models.College> College { get; set; }

        public DbSet<StudentManagementSystem_Web_API_Finale.Models.StudentRegistration> StudentRegistration { get; set; }

        public DbSet<StudentManagementSystem_Web_API_Finale.Models.Enrollment> Enrollment { get; set; }

        public DbSet<StudentManagementSystem_MVC_Finale.Models.Course> Course { get; set; }
    }
}

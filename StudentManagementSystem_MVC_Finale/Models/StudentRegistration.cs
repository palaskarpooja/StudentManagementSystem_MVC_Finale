using StudentManagementSystem_MVC_Finale.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementSystem_Web_API_Finale.Models
{
    public partial class StudentRegistration
    {
        public StudentRegistration()
        {
            Enrollments = new HashSet<Enrollment>();
            Feedbacks = new HashSet<Feedback>();
        }

        public byte Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ContactNumber { get; set; }
        public byte? CollegeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public virtual College College { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}

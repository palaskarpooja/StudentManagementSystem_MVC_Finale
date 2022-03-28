
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace StudentManagementSystem_MVC_Finale.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            Feedbacks = new HashSet<Feedback>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string Fees { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}

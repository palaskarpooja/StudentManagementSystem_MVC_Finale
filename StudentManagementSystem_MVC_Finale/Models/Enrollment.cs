using StudentManagementSystem_MVC_Finale.Models;
using System;

namespace StudentManagementSystem_Web_API_Finale.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public byte StudentId { get; set; }
        public byte CourseId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentRegistration Student { get; set; }
    }
}
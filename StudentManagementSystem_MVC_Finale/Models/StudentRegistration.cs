using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Models
{
    public class StudentRegistration
    {
        public byte Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long ContactNumber { get; set; }
        public byte? CollegeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}

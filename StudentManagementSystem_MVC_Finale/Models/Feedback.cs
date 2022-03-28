using StudentManagementSystem_Web_API_Finale.Models;
using System;

namespace StudentManagementSystem_MVC_Finale.Models
{
public class Feedback
    {
        
            public byte Id { get; set; }
            public string Description { get; set; }
            public byte? CourseId { get; set; }
            public byte? StudentId { get; set; }
            public DateTime FeedbackDate { get; set; }

            public virtual Course Course { get; set; }
            public virtual StudentRegistration Student { get; set; }
        }
    }

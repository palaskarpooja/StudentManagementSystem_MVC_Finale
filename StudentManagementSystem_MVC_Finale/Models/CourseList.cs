using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Models
{
    public class CourseList
    {
        public SelectList ListItem { set; get; }
        public int SelectedItem { set; get; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class StudentController : Controller
    {
        
        
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMyCourses()
        {
            return View();
        }
        public IActionResult ViewCourses()
        {
            return View();
        }
    }
}

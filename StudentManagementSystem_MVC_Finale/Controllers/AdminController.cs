using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetStudentDetails()
        {
            return View();
        }

        public IActionResult GetStudentEnrollment()
        {
            return View();
        }
        public IActionResult GetCurrentEnrollment()
        {
            return View();
        }
    }
}

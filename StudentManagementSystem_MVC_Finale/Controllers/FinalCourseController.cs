using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementSystem_MVC_Finale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class FinalCourseController : Controller
    {

        string Baseurl = "https://localhost:44341/";
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentByCourse()
        {
            List<Course> PInfo = new List<Course>();


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/courses");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Course>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);
            }


        }
    }
}


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagementSystem_MVC_Finale.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using StudentManagementSystem_Web_API_Finale.Models;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class StudentController : Controller
    {
        string Baseurl = "https://localhost:44341/";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMyCourses()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetCourseById(byte id)
        {
            Course PInfo = new Course();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/courses/" + id);
                //HttpResponseMessage Res1 = await client.GetAsync("api/HospitalEmploy/" + id);

                if (Res.IsSuccessStatusCode)
                {

                    var Response = Res.Content.ReadAsStringAsync().Result;

                    PInfo = JsonConvert.DeserializeObject<Course>(Response);
                }
                return View(PInfo);
            }

        }

        [HttpPost]

        public async Task<IActionResult> GetCourseById(byte id, Enrollment enrollment)
        {
            Random random = new Random();
            enrollment.Id = random.Next(100, 1000);
            enrollment.StudentId = 2;
            enrollment.CourseId = id;
            enrollment.CreatedDate = DateTime.Now;

            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(enrollment), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/Enrollment/";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Enrollment"] = JsonConvert.SerializeObject(enrollment);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not update course");
                        return View();
                    }
                }
            }

        }
        public async Task<ActionResult> ListOfCourses()

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

        //PUT - StudentController
        //get data to edit student details
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            List<StudentRegistration> PInfo = new List<StudentRegistration>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var x = HttpContext.Session.GetString("username").ToString();
                HttpResponseMessage Res = await client.GetAsync("api/student/" + x);
                //HttpResponseMessage Res1 = await client.GetAsync("api/HospitalEmploy/" + id);

                if (Res.IsSuccessStatusCode)
                {

                    var Response = Res.Content.ReadAsStringAsync().Result;

                    PInfo = JsonConvert.DeserializeObject<List<StudentRegistration>>(Response);
                }
                return View(PInfo);
            }

        }




        [HttpPost]
        public async Task<IActionResult> Edit(byte id, StudentRegistration studentRegistration)
        {

            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(studentRegistration), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/Student/putstudent/" + id;
                using (var Response = await client.PutAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["StudentRegistration"] = JsonConvert.SerializeObject(studentRegistration);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not update course");
                        return View();
                    }
                }
            }
        }

    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentManagementSystem_MVC_Finale.Models;
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class AdminController : Controller
    {
        string Baseurl = "https://localhost:44341/";
        

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentDetails()
        {
            List<StudentRegistration> PInfo = new List<StudentRegistration>();


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/register");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<StudentRegistration>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);

            }
        }

        


        [HttpGet]
        public async Task<IActionResult> GetStudentEnrollment()
        {
            List<Enrollment> PInfo = new List<Enrollment>();


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Enrollment");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Enrollment>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetCourseList()
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
                HttpResponseMessage Res = await client.GetAsync("api/Course");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Course>>(Response);
                }
                ViewBag.Id = new SelectList(PInfo, "Id", "Name");
                return View();
            }
                      
        }
    





        [HttpPost]
        public async Task<ActionResult> GetStudentByCourseId(byte id)
        {

            List<StudentRegistration> PInfo = new List<StudentRegistration>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/courses/studentcourseenrollment/" + id);


                if (Res.IsSuccessStatusCode)
                {

                    var Response = Res.Content.ReadAsStringAsync().Result;

                    PInfo = JsonConvert.DeserializeObject<List<StudentRegistration>>(Response);
                }
                return View(PInfo);
            }

        }
        /*[HttpGet]
        public async Task<ActionResult> CourseDetails()

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

            }*/


        [HttpGet]
        public IActionResult GetStudentByCourseName(byte id)
        {


            return View();
        }


        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(Course course)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Courses";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Course"] = JsonConvert.SerializeObject(course);
                        return RedirectToAction("Index");
                        ViewData["Message"] = "Successfully added";
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add course");
                        return View();

                    }
                }
            }
        }

        [HttpGet] // and also create a post method
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/admin/validate";


                using (var Response = await client.PostAsync(endpoint, content))
                {
                    string token = await Response.Content.ReadAsStringAsync();
                    if (token == "User not found")
                    {
                        ViewData["message"] = "Invalid Credentials";
                        return View();

                    }
                    HttpContext.Session.SetString("Jwtoken", token);
                    // HttpContext.Session.SetString("UserID", getuserid(user.Email).ToString());
                    // string x = HttpContext.Session.GetString("UserID");
                }
                return Redirect("~/Admin/index");
            }
        }
    }
}



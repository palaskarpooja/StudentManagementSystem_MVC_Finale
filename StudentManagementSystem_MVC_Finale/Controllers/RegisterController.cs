using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentManagementSystem_MVC_Finale.Models;
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class RegisterController : Controller
    {
        string Baseurl = "https://localhost:44341/";
        

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            List<College> PInfo = new List<College>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/College");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<College>>(Response);
                }
                ViewBag.CollegeId = new SelectList(PInfo, "Id", "Name");
                return View();

            }
        }

        [HttpPost]

        public async Task<IActionResult> AddStudent(StudentRegistration studentRegistration)
        {
            studentRegistration.CreatedDate = DateTime.Now;
            

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(studentRegistration), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Register";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        TempData["StudentRegistration"] = JsonConvert.SerializeObject(studentRegistration);
                        return RedirectToAction("Index1", "Home");

                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add Student");
                        return View();

                    }
                   
                     
                }


            }

            
        }

        [HttpGet]

        public IActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> StudentLogin(StudentLogin studentLogin)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(studentLogin), Encoding.UTF8, "application/json");
                
                string endpoint = Baseurl + "api/Register/Validate";

                
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    string token = await Response.Content.ReadAsStringAsync();
                    if (token == "User not found")
                    {
                        ViewData["message"] = "Invalid Credentials";
                        return View();
                        

                    }

                   
                    HttpContext.Session.SetString("Jwtoken", token);
                    HttpContext.Session.SetString("username", studentLogin.Username);
                }
                return Redirect("~/Student/Index");
            }
        }

        [HttpPost]
        public ActionResult LogOut()
        {

            HttpContext.Session.Remove("Jwtoken");

            return RedirectToAction("Index1", "home");
        }

        [HttpGet]
        public async Task<ActionResult> Details(byte id)
        {
            StudentRegistration PInfo = new StudentRegistration();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/StudentLogin/" + id);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<StudentRegistration>(Response);
                }
                //returning the employee list to view
                return View(PInfo);
            }
        }

        [HttpGet]
        public async Task<ActionResult> MyCourses()
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
                var x = HttpContext.Session.GetString("username").ToString();
                HttpResponseMessage Res = await client.GetAsync("api/Courses/mycourse/" + x);
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
    


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
    public class EnrollmentController : Controller
    {
        string Baseurl = "https://localhost:44341/";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> GetEnrollment()
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
        public async Task<IActionResult> TodaysEnrollment()
        {
            List<TodaysEnrollment> PInfo = new List<TodaysEnrollment>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Enrollment/todaysenrollment");
                //Checking the response is successful or not which is sent using HttpClient

                

                if (Res.IsSuccessStatusCode)
                {
                   
                        //Storing the response details recieved from web api
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        PInfo = JsonConvert.DeserializeObject<List<TodaysEnrollment>>(Response);
                   
                    
                }
                //returning the employee list to view
                return View(PInfo);

            }

        }


        [HttpGet]
        public async Task <IActionResult> AddEnrollment()

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
                ViewBag.CourseId = new SelectList(PInfo, "Id", "Name");
                
                return View();

            }
          

        }

        [HttpPost]

        public async Task<IActionResult> AddEnrollment(Enrollment enrollment)
        {
            enrollment.CreatedDate = DateTime.Now;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(enrollment), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Enrollment";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        TempData["StudentRegistration"] = JsonConvert.SerializeObject(enrollment);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add department");
                        
                        return View();

                    }
                }
            }

        }
    }
}

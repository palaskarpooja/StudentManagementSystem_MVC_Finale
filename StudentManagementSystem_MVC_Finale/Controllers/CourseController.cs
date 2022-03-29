using Microsoft.AspNetCore.Mvc;
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
    public class CourseController : Controller
    {
       
        string Baseurl = "https://localhost:44341/";

        public async Task<ActionResult> Index()

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

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AddCourse(Course course)
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

        [HttpGet]
        public async Task<ActionResult> Edit(byte id)
        {
            Course PInfo = new Course();
         
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/courses" + id);
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
        public async Task<IActionResult> Edit(byte id, Course course)
        {

         using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
                string endpoint = this.Baseurl + "api/Courses/" + id;
                using (var Response = await client.PutAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Course"] = JsonConvert.SerializeObject(course);

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

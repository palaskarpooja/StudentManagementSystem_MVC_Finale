using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_MVC_Finale.Controllers
{
    public class CollegeController : Controller
    {
        string Baseurl = "https://localhost:44341/";
        // GET: CollegeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CollegeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollegeController/Create
        public async Task <ActionResult> ViewCollege()
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
                //returning the employee list to view
                return View(PInfo);

            }
    }
        [HttpGet]
        public IActionResult AddCollege()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCollege(College college)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(college), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/College";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["College"] = JsonConvert.SerializeObject(college);
                        return RedirectToAction("ViewCollege");
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



        // POST: CollegeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: CollegeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CollegeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CollegeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CollegeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

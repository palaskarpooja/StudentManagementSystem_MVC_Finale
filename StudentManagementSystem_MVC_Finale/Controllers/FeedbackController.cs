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
    public class FeedbackController : Controller
    {
        string Baseurl = "https://localhost:44341/";
        public async Task<ActionResult> ViewFeedback()
        {
            List<Feedback> PInfo = new List<Feedback>();


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Feedback");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    PInfo = JsonConvert.DeserializeObject<List<Feedback>>(Response);
                }
                //returning the employee list to view
                return View(PInfo);

            }

            
        }

        [HttpGet]
        public IActionResult AddFeedback()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(Feedback feedback)
        {
           feedback.FeedbackDate = DateTime.Now;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endpoint = Baseurl + "api/Feedback";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.IsSuccessStatusCode)
                    {
                        TempData["Feedback"] = JsonConvert.SerializeObject(feedback);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Could not add feedback");
                        return View();

                    }
                }
            }
        }
    }
}

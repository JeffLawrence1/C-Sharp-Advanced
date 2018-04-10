using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using formSubmission_project.Models;

namespace formSubmission_project.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Vali(string firstName, string lastName, int age, string email, string password)
        {
        User NewUser = new User
        {
         firstName = firstName,
         lastName = lastName,
         age = age,
         Email = email,
         Password = password
        };
        TryValidateModel(NewUser);
        ViewBag.errors = ModelState.Values;
        return View("success");
        }
    }
}

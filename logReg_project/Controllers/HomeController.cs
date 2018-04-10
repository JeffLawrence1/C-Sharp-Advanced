using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using logReg_project.Models;

namespace logReg_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }
 
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // Other code
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User newUser){
            
            if(ModelState.IsValid){
                string query = $"INSERT INTO users (firstname, lastname, email, password, created_at) VALUES ('{newUser.FirstName}', '{newUser.LastName}', '{newUser.Email}', '{newUser.Password}', NOW())";
                _dbConnector.Execute(query);
                return View("Success");
            }
            else{
            
                return View("Index");
            }
            
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){
            if(Email != null && Password != null){
                
                string query = $"SELECT * FROM users WHERE email = '{Email}' and password = '{Password}'";

                List<Dictionary<string, object>> result = _dbConnector.Query(query);
                foreach (var dict in result){
                Console.WriteLine(dict["firstname"]);
                }
                
                return View("Success");
            }
            else{

            ViewBag.Errors = "Invalid Name or Password";

            return View("Index");
        }
        }

        // GET: /Home/
        // [HttpGet]
        // [Route("")]
        // public IActionResult Index()
        // {
        //     return View();
        // }
    }
}

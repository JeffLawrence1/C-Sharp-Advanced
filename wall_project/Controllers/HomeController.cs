using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wall_project.Models;
using Newtonsoft.Json;

namespace wall_project.Controllers
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
                string query = $"INSERT INTO users (firstname, lastname, email, password, createdat) VALUES ('{newUser.FirstName}', '{newUser.LastName}', '{newUser.Email}', '{newUser.Password}', NOW())";
                _dbConnector.Execute(query);                
                return View("Index");
            }
            else{            
                return View("Index");
            }            
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){
                
                string query = $"SELECT * FROM users WHERE email = '{Email}' and password = '{Password}'";
                List<Dictionary<string, object>> result = _dbConnector.Query(query);
                if(result.Count == 0 || Password == null || (string)result[0]["password"] != Password){
                    ViewBag.Errors = "Invalid Name or Password, please register or enter the correct info";
                    return View("Index");
                }
                else{
                // foreach (var dict in result){
                // // Console.WriteLine(dict["firstname"]);
                // object firstname = dict["firstname"];
                // ViewBag.firstname = firstname;
                HttpContext.Session.SetInt32("id", (int)result[0]["id"]);                
                }
                return RedirectToAction("dash");
            }
        

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("dash")]
        public IActionResult Dash(){
            if(HttpContext.Session.GetInt32("id") == null){
                return RedirectToAction("Index");
            }
            
            ViewBag.messages = _dbConnector.Query(@"SELECT messages.id AS messages_id, messages.message, messages.createdat, users.firstname, users.lastname 
                             FROM messages JOIN users ON messages.users_id = users.id");
            ViewBag.comments = _dbConnector.Query($@"SELECT comments.id AS comments_id, comments.comment, comments.createdat, users.firstname, users.lastname, comments.messages_id
                              FROM comments JOIN messages ON comments.messages_id = messages.id
                              JOIN users ON comments.users_id = users.id");
            ViewBag.firstname = _dbConnector.Query($"SELECT firstname FROM users WHERE users.id = {(int)HttpContext.Session.GetInt32("id")}")[0];
            return View("Wall");
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddQuote(string message)
        {

            string query = $"INSERT INTO messages (message, users_id, createdat) VALUES ('{message}', {(int)HttpContext.Session.GetInt32("id")},NOW())";
            _dbConnector.Execute(query);
            
            return RedirectToAction("dash");
        }
        [HttpPost]
        [Route("comment")]
        public IActionResult AddComment(string comment, int mess)
        {
            string query = $@"INSERT INTO comments (comment, users_id, messages_id, createdat, updatedat) VALUES ('{comment}', {(int)HttpContext.Session.GetInt32("id")}, '{mess}', NOW(), NOW())";
            _dbConnector.Execute(query);
            return RedirectToAction("dash");
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int mess){
            string query = $"DELETE FROM messages WHERE id='{mess}'";
            _dbConnector.Execute(query);
            return RedirectToAction("dash");
        }
    }

}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace quotingDojo_project.Controllers
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
        // // GET: /Home/
        [HttpGet]
        [Route("skip")]
        public IActionResult Skip()
        {
            string query = "SELECT * FROM users ORDER BY -created_at";
            var allQuotes = _dbConnector.Query(query);
            
            ViewBag.allQuotes = allQuotes;
            return View("Quotes");
        }
        [HttpPost]
        [Route("process")]
        public IActionResult AddQuote(string name, string quote)
        {
            Console.WriteLine(name);
            Console.WriteLine(quote);
            string query = $"INSERT INTO users (name, quote, created_at) VALUES ('{name}', '{quote}', NOW())";
            _dbConnector.Execute(query);
            return RedirectToAction("skip");
        }
    }
}
// List<Dictionary<string, object>> AllUsers = _dbConnector.Query("SELECT * FROM users");

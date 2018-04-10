using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace pokeinfo_project.Controllers
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
        
        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult QueryPoke(int pokeid)
        {
            var PokeInfo = new Pokemon();
            WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                }
            ).Wait();
            // Other code
            ViewBag.Pokemon = PokeInfo;
            return View("Index");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Menuetti.Controllers
{
    public class DietTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // diettype/omni
        public IActionResult Omni()
        {
            List<int> recipes = new List<int>();
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 5; i++)
            {
                recipes.Add(rnd.Next(1, 10));
            }
            return View(recipes);
        }
    }
}
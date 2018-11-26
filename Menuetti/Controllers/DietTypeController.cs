using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Menuetti.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Menuetti.Controllers
{
    public class DietTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // diettype/omni
        //public IActionResult Omni()
        //{
        //    List<int> recipes = new List<int>();
        //    Random rnd = new Random((int)DateTime.Now.Ticks);
        //    for (int i = 0; i < 5; i++)
        //    {
        //        recipes.Add(rnd.Next(1, 10));
        //    }
        //    return View(recipes);
        //}


        private readonly MenuettiDBContext _context;


        public DietTypeController(MenuettiDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Omni()
        {
            var recipes = await _context.Recipes.ToListAsync();
            
            List<string> recipeList = new List<string>();
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                int index = rnd.Next(recipes.Count);
                recipeList.Add(recipes[index].RecipeName);
            }

            return View(recipeList);
        }

}
}
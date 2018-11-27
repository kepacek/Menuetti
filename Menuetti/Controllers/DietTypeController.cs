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
   
        private readonly MenuettiDBContext _context;

        public DietTypeController(MenuettiDBContext context)
        {
            _context = context;
        }

        // diettype/omni
        //public async Task<IActionResult> Omni()
        //{
        //    var recipes = await _context.Recipes.ToListAsync();
            
            
            List<Recipes> recipeList = new List<Recipes>();
            Random rnd = new Random();

            foreach (var item in recipes)
            {
                int index = rnd.Next(recipes.Count);
                recipeList.Add(recipes[index]);
            }
            //for (int i = 0; i < 100; i++)
            //{
            //    int index = rnd.Next(recipes.Count);
            //    recipeList.Add(recipes[index].RecipeName);
            //}


        //    return View(recipeList);
        //}

        // diettype/vegetarian
        //public async Task<IActionResult> Vegetarian()
        //{
        //    var recipes = await _context.Recipes.ToListAsync();

        //    List<string> recipeList = new List<string>();
        //    Random rnd = new Random();

        //    foreach (var item in recipes)
        //    {
        //        int index = rnd.Next(recipes.Count);
        //        recipeList.Add(recipes[index].RecipeName);
        //    }

        //    return View(recipeList);
        //}

        //// diettype/vegan
        //public async Task<IActionResult> Vegan()
        //{
        //    var recipes = await _context.Recipes.ToListAsync();

        //    List<string> recipeList = new List<string>();
        //    Random rnd = new Random();

        //    foreach (var item in recipes)
        //    {
        //        int index = rnd.Next(recipes.Count);
        //        recipeList.Add(recipes[index].RecipeName);
        //    }

        //    return View(recipeList);
        //}
    }
}
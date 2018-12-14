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
        public async Task<IActionResult> Omni()
        {
            var recipes = await _context.Recipes
                            .Include(r => r.Ingredients)
                            .ToListAsync();

            List<Recipes> recipeList = new List<Recipes>();

            foreach (var item in recipes)
            {
                recipeList.Add(item);
            }
            Random rng = new Random();
            int n = recipeList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Recipes value = recipeList[k];
                recipeList[k] = recipeList[n];
                recipeList[n] = value;
            }

                //Random rnd = new Random();

                //foreach (var item in recipes)
                //{
                //    int index = rnd.Next(recipes.Count);
                //    recipeList.Add(recipes[index]);
                //}
                //for (int i = 0; i < 100; i++)
                //{
                //    int index = rnd.Next(recipes.Count);
                //    recipeList.Add(recipes[index].RecipeName);
                //}


                return View(recipeList);
        }


        // diettype/SekaruokaKaruselli
        public async Task<IActionResult> SekaruokaKaruselli(int id = 2)
        {
            var recipes = await _context.Recipes
                            .Include(r => r.Ingredients)
                            .ToListAsync();

            List<Recipes> recipeList = new List<Recipes>();

            foreach (var item in recipes)
            {
                recipeList.Add(item);
            }
            Random rng = new Random();
            int n = recipeList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Recipes value = recipeList[k];
                recipeList[k] = recipeList[n];
                recipeList[n] = value;
            }

            ViewBag.amountOfRecipes = id;
            return View(recipeList);
        }

        // diettype/vegetarian
        public async Task<IActionResult> Vegetarian()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();

            List<Recipes> recipeList = new List<Recipes>();

            foreach (var item in recipes)
            {

                if (item.DietType == "Kasvis" || item.DietType == "Vegaaninen")
                {
                    recipeList.Add(item);
                }
            }
            Random rng = new Random();
            int n = recipeList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Recipes value = recipeList[k];
                recipeList[k] = recipeList[n];
                recipeList[n] = value;
            }
            //Random rnd = new Random();

            //foreach (var item in recipes)
            //{
            //    int index = rnd.Next(recipes.Count);

            //    if (recipes[index].DietType == "Kasvis" || recipes[index].DietType == "Vegaaninen")
            //    {
            //        recipeList.Add(recipes[index]);
            //    }
            //}

            return View(recipeList);
        }

        // diettype/vegan
        public async Task<IActionResult> Vegan()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();

            List<Recipes> recipeList = new List<Recipes>();

            foreach (var item in recipes)
            {

                if (item.DietType == "Vegaaninen")
                {
                    recipeList.Add(item);
                }
            }

            Random rng = new Random();

            int n = recipeList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Recipes value = recipeList[k];
                recipeList[k] = recipeList[n];
                recipeList[n] = value;
            }


            //  Random rnd = new Random();


            //  foreach (var item in recipes)
            //  {
            //    int index = rnd.Next(recipes.Count);

            //    if (recipes[index].DietType == "Vegaaninen")
            //    {
            //        recipeList.Add(recipes[index]);
            //    }
            //}

            return View(recipeList);
        }
    }
}

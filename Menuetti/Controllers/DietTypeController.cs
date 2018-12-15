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


        // diettype/RecipeCarousel/3
        public async Task<IActionResult> RecipeCarousel(int id = 3)
        {
            // Defining the carousel "settings"
            int maxAmountOfRecipes = 5;
            int recipesPerCarousel = 3;

            if (id > maxAmountOfRecipes)
                id = maxAmountOfRecipes;

            if (id < 1)
                id = 3;

            int amountOfRecipesNeeded = id * recipesPerCarousel;

            // get random amount of recipes + their ingredients from the db
            var recipeAmountInDb = _context.Recipes.Count();
            var skipNumbers = Enumerable.Range(1, recipeAmountInDb - 1);
            Random rnd = new Random();
            var skipNumbersRandomised = skipNumbers.OrderBy(x => rnd.Next()).Take(amountOfRecipesNeeded);

            List<Recipes> recipesAndIncredients = new List<Recipes>();

            foreach (var randomNumber in skipNumbersRandomised)
            {
                var recipe = await _context.Recipes
                    .Skip(randomNumber)
                    .Take(1)
                    .Include(r => r.Ingredients)
                    .FirstOrDefaultAsync();

                recipesAndIncredients.Add(recipe);
            } // Could this be done faster?
            
            ViewBag.amountOfRecipes = id;
            return View(recipesAndIncredients);
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

            ViewBag.amountOfRecipes = 3;
            return View("RecipeCarousel", recipeList);
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

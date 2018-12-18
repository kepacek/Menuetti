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
        // Carousel "settings"
        static int maxAmountOfRecipes = 5; // max amount of recipes the user can request. Static until we have enough vegan recipes and we can make it const.
        const int recipesPerCarousel = 3; // how many recipes will be displayed per carousel
        const int defaultNumberOfRecipes = 3; // how many recipe carousels you will get if you haven't chosen the number

        public DietTypeController(MenuettiDBContext context) { _context = context; }
        private readonly MenuettiDBContext _context;

        public IActionResult Index()
        {
            return View();
        }

        // diettype/Omni/3
        public async Task<IActionResult> Omni(int id = 3)
        {
            if (id > maxAmountOfRecipes)
                id = maxAmountOfRecipes;
            else if (id < 1)
                id = defaultNumberOfRecipes;

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
            }

            ViewBag.dietType = "Seka";
            ViewBag.dietUrl = "Omni";
            ViewBag.amountOfRecipes = id;
            return View("RecipeCarousel", recipesAndIncredients);
        }

        // diettype/vegetarian/3
        public async Task<IActionResult> Vegetarian(int id = 3)
        {
            if (id > maxAmountOfRecipes)
                id = maxAmountOfRecipes;
            else if (id < 1)
                id = defaultNumberOfRecipes;

            // This collects every single relevant recipe from the db. Atm it does not matter but when we get more
            // recipes we should make this more efficient.
            var recipes = await _context.Recipes
                .Where(r => r.DietType == "Kasvis" || r.DietType == "Vegaaninen")
                .Include(r => r.Ingredients)
                .ToListAsync();                      

            Random rng = new Random();
            int n = recipes.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Recipes value = recipes[k];
                recipes[k] = recipes[n];
                recipes[n] = value;
            }

            ViewBag.dietType = "Kasvis";
            ViewBag.dietUrl = "Vegetarian";
            ViewBag.amountOfRecipes = id;
            return View("RecipeCarousel", recipes);
        }

        // diettype/vegan/3
        public async Task<IActionResult> Vegan(int id = 3)
        {            
            int maxAmountOfRecipes = 4;
            // THE MAX AMOUNT IS 4 ATM SINCE WE DO NOT HAVE ENOUGH VEGAN RECIPES AND WE DO NOT WANT IT TO CRASH! 

            if (id > maxAmountOfRecipes)
                id = maxAmountOfRecipes;
            else if (id < 1)
                id = defaultNumberOfRecipes;

            // This collects every single relevant recipe from the db. Atm it does not matter but when we get more
            // recipes we should make this more efficient.
            var recipes = await _context.Recipes
                .Where(r => r.DietType == "Vegaaninen")
                .Include(r => r.Ingredients)
                .ToListAsync();

            Random rng = new Random();
            int n = recipes.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Recipes value = recipes[k];
                recipes[k] = recipes[n];
                recipes[n] = value;
            }

            ViewBag.dietType = "Vegaani";
            ViewBag.dietUrl = "Vegan";
            ViewBag.amountOfRecipes = id;
            return View("RecipeCarousel", recipes);
        }
    }
}

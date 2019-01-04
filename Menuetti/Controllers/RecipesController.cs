using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Menuetti.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Menuetti.Controllers
{
    public class RecipesController : Controller
    {
        private readonly MenuettiDBContext _context;

        public RecipesController(MenuettiDBContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            if (User.Claims.Count() > 0)
            {
                string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                ViewBag.UserId = UserId;
            }
            var menuettiDBContext = await _context.Recipes
                .Include(r => r.User)
                .ToListAsync();

            return View(menuettiDBContext);
        }

        // GET: Recipes/UserRecipes
        public async Task<IActionResult> UserRecipes()
        {
            string UserId = null;
            try
            {
                if (User.Claims.Count() > 0)
                {
                    UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    ViewBag.UserId = UserId;
                    var menuettiDBContext = _context.Recipes.Where(r => r.UserId == UserId);
                    return View(await menuettiDBContext.ToListAsync());
                }
                else
                { return View("LoginRequired"); }
            }
            catch (Exception)
            {
                return View("NotFound");
            }
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var recipes = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipes == null)
            {
                return View("NotFound");
            }
            ViewBag.NameOfRecipe = recipes.RecipeName;
            return View(recipes);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> DetailsCarousel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipes == null)
            {
                return NotFound();
            }
            ViewBag.CarouselIngredients = recipes.Ingredients.ToList();
            return View(recipes);
        }

        // GET: Recipes/DetailsShoppinglist/5
        public async Task<IActionResult> DetailsShoppinglist(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var recipes = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipes == null)
            {
                return View("NotFound");
            }

            return View(recipes);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            if (User.Claims.Count() == 0)
            {
                return View("LoginRequired");
            }
            else
            {
                TempData["ingredients"] = GetIngredientList();
                ViewData["UserId"] = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return View();
            }
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,UserId,RecipeName,Portions,Instructions,Time,DietType,Ingredients")] Recipes recipe, ICollection<Ingredients> ingredients, string submitButton)
        {
            if (User.Claims.Count() == 0)
                return View("NotFound");

            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                recipe.UserId = userId;
                _context.Add(recipe);

                await _context.SaveChangesAsync();

                //bool showBadge = ShowBadgeMessage(recipes.DietType, recipes.UserId);
                //if (showBadge)
                //{ return RedirectToAction("Profile", "Account"); }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.AlreadyAddedIngredients = ingredients.Reverse().ToList();
            TempData["ingredients"] = GetIngredientList();
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            else if (User.Claims.Count() == 0)
            {
                return View("LoginRequired");
            }
            else
            {
                string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var recipes = await _context.Recipes
                    .Include(r => r.User)
                    .Include(r => r.Ingredients)
                    .FirstOrDefaultAsync(m => m.RecipeId == id && m.UserId == UserId);
                if (recipes == null)
                {
                    return View("NoPermission");
                }

                TempData["ingredients"] = GetIngredientList();
                ViewBag.UserId = UserId;
                ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", recipes.UserId);

                return View(recipes);
            }
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeId,UserId,RecipeName,Portions,Instructions,Time,DietType,Ingredients")] Recipes recipe, ICollection<Ingredients> ingredients)
        {
            if (id != recipe.RecipeId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // deleting old ingredients from the recipe
                    var oldIngredients = from rec in _context.Ingredients
                                         where rec.RecipeId == recipe.RecipeId
                                         select rec;

                    foreach (var ingredient in oldIngredients)
                    {
                        _context.Remove(ingredient);
                    }

                    _context.Update(recipe);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipesExists(recipe.RecipeId))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", recipe.UserId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            else if (User.Claims.Count() == 0)
            {
                return View("LoginRequired");
            }
            else
            {
                string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var recipes = await _context.Recipes
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(m => m.RecipeId == id && m.UserId == UserId);
                if (recipes == null)
                {
                    return View("NoPermission");
                }

                return View(recipes);
            }
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipes = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult<Ingredientti> LoadJson()
        {
            using (StreamReader r = new StreamReader("ingredients.json"))
            {
                string json = r.ReadToEnd();
                List<Ingredientti> items = JsonConvert.DeserializeObject<List<Ingredientti>>(json);
                return View(items);
            }
        }

        [NonAction]
        private bool RecipesExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }

        [NonAction]
        private List<Ingredientti> GetIngredientList()
        {
            List<Ingredientti> items = new List<Ingredientti>();

            // get the ingredient options list
            using (StreamReader r = new StreamReader("ingredients.json"))
            {
                string ingredients = r.ReadToEnd(); // Read ingredients.json
                string ingredientsEdited = ingredients.Remove(ingredients.Length - 1).ToString(); // Remove the final "]" from the ingredients.json file to prepare the string for concatenation
                string json = string.Concat(ingredientsEdited, ",", AddedIngredients().Substring(1).ToString()); // Concatenate the above ingredients-string with a "," and an addedIngredients string with the initial "[" removed
                items = JsonConvert.DeserializeObject<List<Ingredientti>>(json).ToList(); // Create a list of ingredients
            }
            return items;
        }

        [NonAction]
        public string AddedIngredients()
        {
            // Create a string from the non-Fineli API ingredients located in the ingredientsAdded.json file
            using (StreamReader r = new StreamReader("ingredientsAdded.json"))
            {
                string addedIngredients = r.ReadToEnd();
                return addedIngredients;
            }
        }

        [NonAction]
        private bool ShowBadgeMessage(string DietType, string UserId)
        {
            var userrecipes = _context.Recipes.Where(r => r.UserId == UserId).ToList();
            //Linq for checking the amount of recipes byt DietType added by user
            var result = (from s in userrecipes
                          where s.DietType == DietType
                          select s).Count();

            if (result == 3)
                { return true; }
            else if (result == 10)
                { return true; }
            else if (result == 20)
                { return true; }
            else
                { return false; }
        }
    }
}

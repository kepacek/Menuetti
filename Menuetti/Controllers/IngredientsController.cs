using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Menuetti.Models;
using System.Security.Claims;
using Newtonsoft.Json;
using System.IO;

namespace Menuetti.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly MenuettiDBContext _context;

        public IngredientsController(MenuettiDBContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            string UserId = null;
            try
            {
                if (User.Claims.Count() > 0)
                {
                    UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    ViewBag.UserId = UserId;
                }
                else
                { return View("LoginRequired"); }
                var menuettiDBContext = _context.Ingredients.Include(i => i.Recipe).Where(r => r.Recipe.UserId == UserId);
                return View(await menuettiDBContext.ToListAsync());
            }
            catch (Exception)
            {
                return View("NotFound");
            }

        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var ingredients = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            if (ingredients == null)
            {
                return View("NotFound");
            }
            if (User.Claims.Count() > 0)
            {
                string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                ViewBag.UserId = UserId;
            }

            return View(ingredients);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            ViewData["RecipeId"] = new SelectList(_context.Recipes.Where(r => r.UserId == UserId), "RecipeId", "RecipeName");
            //ViewData["IngredientName"] = new SelectList(LoadJson(), "name.fi");
            ViewBag.Json = new SelectList(LoadJson(), "name.fi", "name.fi");
            //ViewBag.units = new SelectList(LoadJson(), "units.Select(z=>z.description.fi))", "units.Select(z=>z.description.fi))");
            return View();

        }
        public IActionResult CreateToRecipe(int RecipeId)
        {
            var rec = _context.Recipes.Find(RecipeId);
            ViewBag.RecipeName = rec.RecipeName;
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeName");
            SelectList JsonLista = new SelectList(LoadJson(), "name.fi", "name.fi");
            ViewBag.Json = JsonLista;
            return View();
        }

        public string AddedIngredients()
        {
            // Create a string from the non-Fineli API ingredients located in the ingredientsAdded.json file
            using (StreamReader r = new StreamReader("ingredientsAdded.json"))
            {
                string addedIngredients = r.ReadToEnd();
                return addedIngredients;
            }
        }

        public List<Ingredientti> LoadJson()
        {
            using (StreamReader r = new StreamReader("ingredients.json"))
            {
                string ingredients = r.ReadToEnd(); // Read ingredients.json
                string ingredientsEdited = ingredients.Remove(ingredients.Length-1).ToString(); // Remove the final "]" from the ingredients.json file to prepare the string for concatenation
                string json = string.Concat(ingredientsEdited, ",", AddedIngredients().Substring(1).ToString()); // Concatenate the above ingredients-string with a "," and an addedIngredients string with the initial "[" removed
                List<Ingredientti> items = JsonConvert.DeserializeObject<List<Ingredientti>>(json).OrderBy(t => t.name.fi).ToList(); // Create a list of ingredients and added sorted alphabetically by the name
                return items;
            }
        }


        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientId,RecipeId,IngredientName,AmountG,RecipeUnit")] Ingredients ingredients)
        {
            Recipes recipes = new Recipes();
            string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (User.Claims.Count() > 0)
            {
                ViewBag.UserId = UserId;
            }
            if (ModelState.IsValid)
            {
                _context.Add(ingredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string recipe = recipes.RecipeName;
            ViewBag.RecipeName = recipe;
            ViewData["RecipeId"] = new SelectList(_context.Recipes.Where(r => r.UserId == UserId), "RecipeId", "Instructions", ingredients.RecipeId);
            return View(ingredients);
            //var recipelist = _context.Recipes.Where(r => r.UserId == UserId);
            //string recipe = recipelist.ToString();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateToRecipe([Bind("IngredientId,RecipeId,IngredientName,AmountG,RecipeUnit")] Ingredients ingredients)
        {
            string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (User.Claims.Count() > 0)
            {
                ViewBag.UserId = UserId;
            }
            //if (ingredients.Recipe.UserId != UserId)
            //{
            //    return View("NoPermission");
            //}
            if (ModelState.IsValid)
            {
                _context.Add(ingredients);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Recipes", new { id = ingredients.RecipeId });
            }
            Recipes recipes = new Recipes();
            string recipe = recipes.RecipeName;
            ViewBag.RecipeName = recipe;
            ViewData["RecipeId"] = new SelectList(_context.Recipes.Where(r => r.UserId == UserId), "RecipeId", "Instructions", ingredients.RecipeId);

            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            //var ingredient = await _context.Ingredients.FindAsync(id);
            var ingredient = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (ingredient == null || ingredient.Recipe.UserId != UserId)
            {
                return View("NoPermission");
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeName", ingredient.RecipeId);
            ViewBag.Json = new SelectList(LoadJson(), "name.fi", "name.fi");
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IngredientId,RecipeId,IngredientName,AmountG,RecipeUnit")] Ingredients ingredients)
        {
            if (id != ingredients.IngredientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientsExists(ingredients.IngredientId))
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
            ViewBag.Json = new SelectList(LoadJson(), "name.fi", "name.fi");
            var rec = _context.Recipes.Find(id);
            ViewBag.RecipeName = rec.RecipeName;
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "Instructions", ingredients.RecipeId);
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var ingredient = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (ingredient == null || ingredient.Recipe.UserId != UserId)
            {
                return View("NoPermission");
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredients = await _context.Ingredients.FindAsync(id);
            _context.Ingredients.Remove(ingredients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientsExists(int id)
        {
            return _context.Ingredients.Any(e => e.IngredientId == id);
        }
    }
}

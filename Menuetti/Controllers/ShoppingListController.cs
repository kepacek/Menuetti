using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menuetti.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Menuetti.Controllers
{
    public class ShoppingListController : Controller
    {
        //private MenuettiDBContext db = new MenuettiDBContext();

        private readonly MenuettiDBContext _context;
        public ShoppingListController(MenuettiDBContext context)
        {
            _context = context;
        }

        // GET: ShoppingList
        public ActionResult Index()
        {
            return View();
        }

        //GET: ShoppingList/Details/5
        public async Task<IActionResult> Details(int RecipeId)
        {
            List<Recipes> rlist = new List<Recipes>();


            //setting values for id - id4
            Recipes recipe0 = _context.Recipes.Include("Ingredients").Single(r=>r.RecipeId ==  3);
            Recipes recipe1 = _context.Recipes.Include("Ingredients").Single(r=>r.RecipeId ==  6);
            Recipes recipe2 = _context.Recipes.Include("Ingredients").Single(r=>r.RecipeId ==  7);
            Recipes recipe3 = _context.Recipes.Include("Ingredients").Single(r=>r.RecipeId ==  8);
            Recipes recipe4 = _context.Recipes.Include("Ingredients").Single(r => r.RecipeId == 9);

            
            rlist.Add(recipe0);
            rlist.Add(recipe1);
            rlist.Add(recipe2);
            rlist.Add(recipe3);
            rlist.Add(recipe4);

           
            Dictionary<string, int?> shoppings = new Dictionary<string, int?>();

            foreach (var r in rlist)
            {

                foreach (var ing in r.Ingredients)
                {
                    string Key = ing.IngredientName;
                    int? Value;
                    if (shoppings.ContainsKey(ing.IngredientName))
                    {
                        if (ing.AmountG == null)
                        {
                            Value = 0;
                            shoppings[Key] += Value;
                        }
                        else
                        {
                            Value = ing.AmountG;
                            shoppings[Key] +=  Value;
                        }
                    }
                   else
                    {
                        Value = ing.AmountG;
                        shoppings.Add(Key, Value);
                    }
                   
                }
            }
            ViewBag.result = shoppings;
            return View();

        }

        // GET: ShoppingList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingList/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
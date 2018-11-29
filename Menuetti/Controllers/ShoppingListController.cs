using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menuetti.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace Menuetti.Controllers
{
    public class ShoppingListController : Controller
    {
        //private MenuettiDBContext db = new MenuettiDBContext();
        
        public List<Ingredientti> LoadJsoni()
        {
            using (StreamReader r = new StreamReader("ingredients.json"))
            {
                string json = r.ReadToEnd();
                List<Ingredientti> items = JsonConvert.DeserializeObject<List<Ingredientti>>(json);

                return items;

            }
        }


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
        public async Task<IActionResult> ShoppingListDetails(int id1, int id2, int id3, int id4, int id5)
        {
            List<Recipes> rlist = new List<Recipes>();
            List<Ingredientti> jsonidata = LoadJsoni();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //setting values for id - id4
            //Recipes recipe0 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id1);
            //Recipes recipe1 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id2);
            //Recipes recipe2 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id3);
            //Recipes recipe3 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id4);

            Recipes recipe0;
            if (id1 != 0)
            {
                recipe0 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id1);
            }
            else
            { recipe0 = null; };

            Recipes recipe1;
            if (id2 != 0)
            {
                recipe1 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id2);
            }
            else
            { recipe1 = null; };

            Recipes recipe2;
            if (id3 != 0)
            {
                recipe2 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id3);
            }
            else
            { recipe2 = null; };

            Recipes recipe3;
            if (id4 != 0)
            {
                recipe3 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id4);
            }
            else
            { recipe3 = null; };


            Recipes recipe4;
            if (id5 != 0)
            {
                recipe4 = await _context.Recipes.Include("Ingredients").SingleAsync(r => r.RecipeId == id5);
            }
            else
            { recipe4 = null; };


            //rlist.Add(recipe0);
            //rlist.Add(recipe1);
            //rlist.Add(recipe2);
            //rlist.Add(recipe3);

            if (id1 != 0)
            {
                rlist.Add(recipe0);
            };

            if (id2 != 0)
            {
                rlist.Add(recipe1);
            };

            if (id3 != 0)
            {
                rlist.Add(recipe2);
            };

            if (id4 != 0)
            {
                rlist.Add(recipe3);
            };

            if (id5 != 0) {
                rlist.Add(recipe4);};


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
                            shoppings[Key] += Value;
                        }
                    }
                    else
                    {
                        Value = ing.AmountG;
                        shoppings.Add(Key, Value);
                    }

                }
            }
            ViewBag.List = shoppings;
            ViewBag.Recipes = rlist;
            ViewBag.jsoni = jsonidata;


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
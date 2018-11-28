using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Menuetti.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Menuetti.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the 
                // **Allowed Logout URLs** settings for the client.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging 
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private readonly MenuettiDBContext _context;
        public AccountController(MenuettiDBContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {

            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var recipes = await _context.Recipes.Where(r => r.UserId == userId).ToListAsync();

            //Linq for checking the amount of recipes byt DietType added by user
            var resultVegan = (from s in recipes
                               where s.DietType == "Vegaaninen"
                               select s).Count();

            var resultOmni = (from s in recipes
                              where s.DietType == "Sekaruoka"
                              select s).Count();

            var resultVegetarian = (from s in recipes
                                    where s.DietType == "Kasvis"
                                    select s).Count();

            //ViewsBag for the amount of vegan recipes by user
            ViewBag.Vegan = resultVegan;
            //ViewsBag for the amount of omni recipes by user sekaruoka
            ViewBag.Omni = resultOmni;
            //ViewsBag for the amount of vegetarian recipes by user kasvis
            ViewBag.Vegetarian = resultVegetarian;

            return View(new UserProfileViewModel()
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
    }
}
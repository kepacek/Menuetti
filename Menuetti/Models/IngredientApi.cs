using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Menuetti.Models
{
    public class Ingredient
    {
        public Name name;
        public int id;
        public IngredientClass ingredientClass;
        public string fiber;
        public string salt;
        public string fat;
        public string carbohydrate;
        public string protein;
        public string energyKcal;
        public string saturatedFat;
        public List<string> specialDiets;
        public List<Unit> units;
    }
    public class Unit
    {
        public string code { get; set; }
        public Description description;
        public string mass;
    }
    public class IngredientClass
    {
        public Description description;
    }
    public class Description
    {
        public string fi { get; set; }
    }
    public class Name
    {
        public string fi { get; set; }
        //public string sv { get;set;}
        //public string en { get;set;}
        //public string la { get;set;}
    }
}

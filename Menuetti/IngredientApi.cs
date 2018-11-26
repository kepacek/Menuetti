using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Menuetti.Models
{
    public class Ingredientti
    {
        public Name name { get; set; }
        public int id{ get; set; }
        public IngredientClass ingredientClass { get; set; }
        public string fiber{ get; set; }
        public string salt{ get; set; }
        public string fat{ get; set; }
        public string carbohydrate{ get; set; }
        public string protein{ get; set; }
        public string energyKcal{ get; set; }
        public string saturatedFat{ get; set; }
        public List<string> specialDiets{ get; set; }
        public List<Unit> units{ get; set; }
    }
    public class Unitti
    {
        public string code { get; set; }
        public Description description;
        public string mass;
    }
    public class IngredientClassi
    {
        public Description description;
    }
    public class Descriptioni
    {
        public string fi { get; set; }
    }
    public class Namei
    {
        public string fi { get; set; }
        //public string sv { get;set;}
        //public string en { get;set;}
        //public string la { get;set;}
    }

 
}

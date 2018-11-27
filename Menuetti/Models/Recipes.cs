using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Menuetti.Models
{
    public partial class Recipes
    {
        public Recipes()
        {
            Ingredients = new HashSet<Ingredients>();
        }

        public int RecipeId { get; set; }
        public string UserId { get; set; }
        public string RecipeName { get; set; }
        public int Portions { get; set; }
        [DataType(DataType.MultilineText)]
        public string Instructions { get; set; }
        public int Time { get; set; }
        public string DietType { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
    }
}

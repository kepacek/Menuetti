using System;
using System.Collections.Generic;

namespace Menuetti.Models
{
    public partial class Ingredients
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public string IngredientName { get; set; }
        public int? AmountG { get; set; }
        public string RecipeUnit { get; set; }

        public virtual Recipes Recipe { get; set; }
    }
}

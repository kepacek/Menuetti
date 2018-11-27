using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menuetti.Models
{
    public class ShoppingList
    {
        
            public int IngredientId { get; set; }
            //public int RecipeId { get; set; }
            public string IngredientName { get; set; }
            public int? AmountG { get; set; }
            public string RecipeUnit { get; set; }

    }
}

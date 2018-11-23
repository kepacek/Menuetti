using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menuetti.Models
{
    public class RecipesViewModel
    {
        public RecipesViewModel()
        {
           
        }

        public Recipes Recipe { get; set; }



        public virtual ICollection<Ingredients> Ingredients { get; set; }

     
    }
}

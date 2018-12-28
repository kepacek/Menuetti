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
        [Required(ErrorMessage = "Sinun on annettava reseptille nimi.")]
        public string RecipeName { get; set; }
        [Required(ErrorMessage = "Annosmäärä vaaditaan.")]
        public int Portions { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Valmistusohjeet vaaditaan.")]
        public string Instructions { get; set; }
        [Required(ErrorMessage = "Valmistusaika vaaditaan.")]
        public int Time { get; set; }
        [Required(ErrorMessage = "Valitse valikosta, millaiselle ruokavaliolle resepti sopii.")]
        public string DietType { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
    }
}

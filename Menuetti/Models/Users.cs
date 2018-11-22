using System;
using System.Collections.Generic;

namespace Menuetti.Models
{
    public partial class Users
    {
        public Users()
        {
            MyLists = new HashSet<MyLists>();
            Recipes = new HashSet<Recipes>();
        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<MyLists> MyLists { get; set; }
        public virtual ICollection<Recipes> Recipes { get; set; }
    }
}

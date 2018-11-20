using System;
using System.Collections.Generic;

namespace Menuetti.Models
{
    public partial class MyLists
    {
        public int ListId { get; set; }
        public string UserId { get; set; }
        public string MenuList { get; set; }
        public string ShoppingList { get; set; }

        public virtual Users User { get; set; }
    }
}

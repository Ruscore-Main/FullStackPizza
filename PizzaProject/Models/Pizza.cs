using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public partial class Pizza
    {
         public Pizza()
        {
            this.Image = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Types { get; set; }
        public string Sizes { get; set; }
        public short Price { get; set; }
        public short Category { get; set; }
        public short Rating { get; set; }

        public virtual ICollection<Image> Image { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int PizzaId { get; set; }

        public virtual Pizza pizza { get; set; }
    }
}

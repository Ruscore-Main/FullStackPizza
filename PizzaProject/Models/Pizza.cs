using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public class Pizza
    {

        public Pizza ()
        {
            this.Images = new List<PizzaImage>();
            this.Pizza_Types = new List<Pizza_Type>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Category { get; set; }
        public int Rating { get; set; }

        public virtual ICollection<PizzaImage> Images { get; set; }
        public List<Pizza_Type> Pizza_Types { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public class PizzaImage
    {
        [Key]
        public int Id { get; set; }
        public int Size { get; set; }
        public string ImageUrl { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public class Pizza_Type
    {
        [Key]
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

    }
}

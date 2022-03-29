using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
    }
}

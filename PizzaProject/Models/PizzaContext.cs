using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaImage> PizzaImages { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Pizza_Type> Pizza_Types { get; set; }


        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza_Type>()
            .HasOne(p => p.Pizza)
            .WithMany(pt => pt.Pizza_Types)
            .HasForeignKey(pi => pi.PizzaId);

            modelBuilder.Entity<Pizza_Type>()
            .HasOne(t => t.Type)
            .WithMany(pt => pt.Pizza_Types)
            .HasForeignKey(pi => pi.TypeId);

            modelBuilder.Entity<PizzaImage>()
                .HasOne(pi => pi.Pizza)
                .WithMany(p => p.Images);
                
        }
    }
}

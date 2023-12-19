using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Final.Data
{
    public class ClotheContext : IdentityDbContext
    {
        public ClotheContext (DbContextOptions<ClotheContext> options)
            : base(options)
        {
        }

        public DbSet<Final.Models.Clothe> Clothe { get; set; } = default!;

        public DbSet<Final.Models.Person> Person { get; set; } = default!;

        public DbSet<Final.Models.Stock> Stock { get; set; } = default!;

        public DbSet<Final.Models.Brand> Brand { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock>()
            .HasOne(i => i.Clothe)
            .WithMany()
            .HasForeignKey(i => i.ClotheId);
            
            modelBuilder.Entity<Clothe>()
            .HasMany(p=> p.Brands)
            .WithMany(p=> p.Clothes)
            .UsingEntity("BrandsClothes");  
        }
    }
}

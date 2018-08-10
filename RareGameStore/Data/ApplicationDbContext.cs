using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RareGameStore.Models;

namespace RareGameStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Platform> Platform { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCart> GameCarts { get; set; }
        public DbSet<GameCartProduct> GameCartProducts { get; set; }
        public DbSet<GameOrder> GameOrders { get; set; }
        public DbSet<GameOrderProduct> GameOrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //Works ok, but I can typo my property name or pick a property that doesn't exist
            //builder.Entity<Platform>().HasKey("Name");

            //Catches errors at compile time
            //builder.Entity<Platform>().HasKey(x => x.Name);

            //Great spot for default values and other rules
            builder.Entity<Platform>().Property(x => x.DateCreated).HasDefaultValueSql("GetDate()");
            builder.Entity<Platform>().Property(x => x.DateLastModified).HasDefaultValueSql("GetDate()");
            builder.Entity<Platform>().Property(x => x.Name).HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.GameCart)
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey<GameCart>(x => x.ApplicationUserID);

            //builder.Entity<BeachCart>()
            //    .HasOne(x => x.ApplicationUser)
            //    .WithOne(x => x.BeachCart)
            //    .HasForeignKey<ApplicationUser>(x => x.BeachCartID);
        }
    }
}

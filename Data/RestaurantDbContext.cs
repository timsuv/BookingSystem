using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data
{
    public class RestaurantDbContext :DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
            
        }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableNumber = 1, Capacity = 2 },
                new Table { TableId = 2, TableNumber = 2, Capacity = 4 },
                new Table { TableId = 3, TableNumber = 3, Capacity = 6 },
                new Table { TableId = 4, TableNumber = 4, Capacity = 8 },
                new Table { TableId = 5, TableNumber = 5, Capacity = 2 }
                );

            modelBuilder.Entity<Administrator>().HasData(
               new Administrator
               {
                   AdminId = 1,
                   Username = "admin",
                   Password = "Admin1234!" 
               }
           );

            // Seed MenuItems
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    MenuItemId = 1,
                    Name = "Pasta Carbonara",
                    Price = 185.00m,
                    Description = "Klassisk italiensk pasta med ägg, parmesan och pancetta",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/carbonara.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 2,
                    Name = "Grillad Lax",
                    Price = 245.00m,
                    Description = "Färsk lax med citron och dillsås, serveras med potatis",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/grilled-salmon.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 3,
                    Name = "Poké Bowl",
                    Price = 165.00m,
                    Description = "Näringsrik skål med quinoa, rostade grönsaker och tahini-dressing",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/buddha-bowl.jpg"
                }
            );
        }
    }
}

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
                   Name = "Antipasto della Casa",
                   Price = 185.00m,
                   Description = "Selection of cured meats, aged cheeses, olives, roasted peppers and artisanal bread",
                   IsPopular = true,
                   BildUrl = "https://example.com/images/antipasto.jpg"
               },
                new MenuItem
                {
                    MenuItemId = 2,
                    Name = "Bruschetta Classica",
                    Price = 95.00m,
                    Description = "Toasted sourdough topped with fresh tomatoes, basil, garlic and extra virgin olive oil",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/bruschetta.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 3,
                    Name = "Burrata con Prosciutto",
                    Price = 165.00m,
                    Description = "Creamy burrata cheese served with Parma ham, arugula and aged balsamic reduction",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/burrata.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 4,
                    Name = "Carpaccio di Manzo",
                    Price = 195.00m,
                    Description = "Thinly sliced raw beef with capers, arugula, Parmigiano-Reggiano and lemon",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/carpaccio.jpg"
                },

                // PRIMI PIATTI (First Courses - Pasta)
                new MenuItem
                {
                    MenuItemId = 5,
                    Name = "Spaghetti Carbonara",
                    Price = 175.00m,
                    Description = "Traditional Roman pasta with guanciale, eggs, Pecorino Romano and black pepper",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/carbonara.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 6,
                    Name = "Penne all'Arrabbiata",
                    Price = 155.00m,
                    Description = "Spicy tomato sauce with garlic, red chili peppers and fresh basil",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/arrabbiata.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 7,
                    Name = "Fettuccine Alfredo",
                    Price = 165.00m,
                    Description = "Fresh fettuccine in rich butter and Parmigiano-Reggiano cream sauce",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/alfredo.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 8,
                    Name = "Lasagne della Nonna",
                    Price = 195.00m,
                    Description = "Traditional layered pasta with Bolognese ragu, béchamel and three cheeses",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/lasagne.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 9,
                    Name = "Linguine alle Vongole",
                    Price = 225.00m,
                    Description = "Fresh linguine with clams, white wine, garlic, parsley and cherry tomatoes",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/vongole.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 10,
                    Name = "Risotto ai Funghi Porcini",
                    Price = 215.00m,
                    Description = "Creamy Arborio rice with wild porcini mushrooms, white wine and Parmigiano",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/risotto.jpg"
                },

                // SECONDI PIATTI (Main Courses)
                new MenuItem
                {
                    MenuItemId = 11,
                    Name = "Osso Buco alla Milanese",
                    Price = 325.00m,
                    Description = "Braised veal shanks in white wine and vegetables, served with saffron risotto",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/ossobuco.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 12,
                    Name = "Saltimbocca alla Romana",
                    Price = 285.00m,
                    Description = "Veal escalopes with prosciutto and sage in white wine sauce",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/saltimbocca.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 13,
                    Name = "Branzino in Crosta di Sale",
                    Price = 295.00m,
                    Description = "Mediterranean sea bass baked in sea salt crust with herbs and lemon",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/branzino.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 14,
                    Name = "Pollo alla Parmigiana",
                    Price = 245.00m,
                    Description = "Breaded chicken breast with tomato sauce, mozzarella and Parmigiano-Reggiano",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/parmigiana.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 15,
                    Name = "Bistecca alla Fiorentina",
                    Price = 485.00m,
                    Description = "Traditional Tuscan T-bone steak grilled with rosemary, garlic and olive oil (700g)",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/fiorentina.jpg"
                },

                // PIZZA (Wood-fired oven pizzas)
                new MenuItem
                {
                    MenuItemId = 16,
                    Name = "Pizza Margherita",
                    Price = 145.00m,
                    Description = "Classic Neapolitan pizza with San Marzano tomatoes, mozzarella di bufala and basil",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/margherita.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 17,
                    Name = "Pizza Quattro Stagioni",
                    Price = 175.00m,
                    Description = "Four seasons pizza with artichokes, mushrooms, ham and olives",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/quattrostagioni.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 18,
                    Name = "Pizza Diavola",
                    Price = 165.00m,
                    Description = "Spicy pizza with tomato, mozzarella, spicy salami and chili peppers",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/diavola.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 19,
                    Name = "Pizza Prosciutto e Funghi",
                    Price = 185.00m,
                    Description = "Pizza with tomato, mozzarella, prosciutto cotto and fresh mushrooms",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/prosciuttofunghi.jpg"
                },

                // DOLCI (Desserts)
                new MenuItem
                {
                    MenuItemId = 20,
                    Name = "Tiramisu della Casa",
                    Price = 95.00m,
                    Description = "Traditional Italian dessert with mascarpone, coffee, cocoa and ladyfingers",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/tiramisu.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 21,
                    Name = "Panna Cotta ai Frutti di Bosco",
                    Price = 85.00m,
                    Description = "Silky vanilla panna cotta with mixed berry compote",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/pannacotta.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 22,
                    Name = "Cannoli Siciliani",
                    Price = 105.00m,
                    Description = "Crispy pastry tubes filled with sweet ricotta, chocolate chips and pistachios",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/cannoli.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 23,
                    Name = "Gelato Artigianale",
                    Price = 75.00m,
                    Description = "Three scoops of artisanal gelato: vanilla, chocolate, and seasonal fruit",
                    IsPopular = true,
                    BildUrl = "https://example.com/images/gelato.jpg"
                },
                new MenuItem
                {
                    MenuItemId = 24,
                    Name = "Affogato al Caffè",
                    Price = 65.00m,
                    Description = "Vanilla gelato 'drowned' in hot espresso with amaretti biscuits",
                    IsPopular = false,
                    BildUrl = "https://example.com/images/affogato.jpg"
                }
            );
        }
    }
}

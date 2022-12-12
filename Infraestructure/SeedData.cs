using Microsoft.EntityFrameworkCore;
using Rinboku.Models;

namespace Rinboku.Infraestructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                Category furniture = new Category { Name = "Furniture", Slug = "furniture" };
                Category ornaments = new Category { Name = "Ornaments", Slug = "ornaments" };
                Category gifts = new Category { Name = "Gifts", Slug = "gifts" };
                Category openings = new Category { Name = "Openings", Slug = "openings" };
                Category sheatings = new Category { Name = "Sheatings", Slug = "sheatings" };
                Category kitchen = new Category { Name = "Kitchen", Slug = "kitchen" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Coffee Table",
                        Slug = "coffee-table",
                        Description = "Reclaimed Solid Wood Unique Rustic Coffee Table • Realtor Closing Gift • Housewarming Gift First Home • Moving Away Gift Unique Furniture",
                        Price = 399.00M,
                        Category = furniture,
                        Image = "coffee-table.jpg"
                    },
                    new Product
                    {
                        Name = "Cutting Board",
                        Slug = "cutting-board",
                        Description = "Beautiful end grain oak cutting board. Perfect gift for kitchen",
                        Price = 65.00M,
                        Category = kitchen,
                        Image = "cutting-board.jpg"
                    },
                    new Product
                    {
                        Name = "Rocking Chair",
                        Slug = "rocking-chair",
                        Description = "rocking/folding chair Wood/Wooden vintage handmade/handcrafted chair unique indoor or outdoor chair modern launge chair living room chair",
                        Price = 3104.10M,
                        Category = furniture,
                        Image = "rocking-chair.jpg"
                    },
                    new Product
                    {
                        Name = "Storage Box",
                        Slug = "storage-box",
                        Description = "Solid Walnut storage box - Wood rectangular jewelry box with lid - Small stash box - Handmade storage box for office, bathroom, bedroom",
                        Price = 20.00M,
                        Category = gifts,
                        Image = "storage-box.jpg"
                    },
                    new Product
                    {
                        Name = "Hummingbird",
                        Slug = "hummingbird",
                        Description = "Wooden Hummingbird Feeding on a Flower, Handmade Sculpture, Wood Carving Figure, Bird Statue, Colibri, Home Decor, Birthday, End Year Gift",
                        Price = 31.41M,
                        Category = ornaments,
                        Image = "hummingbird.jpg"
                    },
                    new Product
                    {
                        Name = "Table Chair",
                        Slug = "table-chair",
                        Description = "Custom Walnut Wood Dining Table Chair",
                        Price = 1400.00M,
                        Category = furniture,
                        Image = "table-chair.jpg"
                    },
                    new Product
                    {
                        Name = "Carved Door",
                        Slug = "carved-door",
                        Description = "Fabulous Moroccan Door, Handmade Berber Door, Custom Door, Free Shipping",
                        Price = 1757.62M,
                        Category = openings,
                        Image = "carved-door.jpg"
                    },
                    new Product
                    {
                        Name = "Dinning Table",
                        Slug = "dinning-table",
                        Description = "Rustic Wooden Table, Kitchen Table, Live Edge Dining Table, Wooden Table, Dining Room Furniture,Kitchen Dining Table, Farmhouse table ,Table",
                        Price = 300.00M,
                        Category = furniture,
                        Image = "dinning-table.jpg"
                    },
                    new Product
                    {
                        Name = "Solid Panels",
                        Slug = "solid-panels",
                        Description = "Solid walnut panels, walnut lumber, bulk blanks, for woodworking and crafts, laser cutting/engraving materials",
                        Price = 31.90M,
                        Category = sheatings,
                        Image = "solid-panels.jpg"
                    },
                    new Product
                    {
                        Name = "Trunk Box",
                        Slug = "trunk-box",
                        Description = "Wooden trunk box furniture treasure Berber Design Trunk Vintage Wooden Chest Box for decoration",
                        Price = 1557.08M,
                        Category = furniture,
                        Image = "trunk-box.jpg"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

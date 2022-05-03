using hakimslivs.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace hakimslivs.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@example.com",
                FirstName = "Admin",
                LastName = "Administrator",
                Street = "The Street",
                StreetNumber = 1,
                PostalCode = 12345,
                City = "The city",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }

            }
        }

        public static async Task InitializeProductAsync(ApplicationDbContext database)
        {
            if (database.Items.Any())
            {
                return;
            }

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Banan",
                Price = 5.5M,
                Stock = 100,
                Description = "En lång gul böjd frukt.",
                ImageURL = "pictures/banana.jpg"
            });

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Päron",
                Price = 6,
                Stock = 200,
                Description = "En grön frukt.",
                ImageURL = "pictures/pear.jpg"
            });

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Apelsin",
                Price = 5.5M,
                Stock = 150,
                Description = "En lång gul böjd frukt.",
                ImageURL = "pictures/orange.jpg"
            });

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Äpple",
                Price = 4,
                Stock = 300,
                Description = "Finns i flera färger.",
                ImageURL = "pictures/apple.jpg"
            });

            database.Items.Add(new Item
            {
                Category = "Dryck",
                Product = "Coca Cola",
                Price = 11,
                Stock = 55,
                Description = "Originalet.",
                ImageURL = "pictures/cocacola.jpg"
            });

            database.Items.Add(new Item
            {
                Category = "Dryck",
                Product = "Red Bull",
                Price = 17,
                Stock = 75,
                Description = "Ger dig vingar.",
                ImageURL = "pictures/redbull.jpg"
            });

            database.Items.Add(new Item
            {
                Category = "Dryck",
                Product = "Mountain Dew",
                Price = 11,
                Stock = 22,
                Description = "Sött vatten.",
                ImageURL = "pictures/mountaindew.jpg"
            });

            database.SaveChanges();
        }
    }
}

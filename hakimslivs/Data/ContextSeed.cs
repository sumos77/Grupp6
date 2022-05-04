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
                ImageURL = "https://images.unsplash.com/photo-1528825871115-3581a5387919?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=415&q=80g"
            });

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Päron",
                Price = 6,
                Stock = 200,
                Description = "En grön frukt.",
                ImageURL = "https://images.unsplash.com/photo-1619506147154-01717498fc26?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80"
            });

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Apelsin",
                Price = 5.5M,
                Stock = 5,
                ImageURL = "https://images.unsplash.com/photo-1608447779172-bcf64548de5c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=870&q=80"
            });

            database.Items.Add(new Item
            {
                Category = "Frukt",
                Product = "Äpple",
                Price = 4,
                Stock = 300,
                Description = "Finns i flera färger.",
                ImageURL = "https://images.unsplash.com/photo-1630563451961-ac2ff27616ab?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=387&q=80"
            });

            database.Items.Add(new Item
            {
                Category = "Dryck",
                Product = "Coca Cola",
                Price = 11,
                Stock = 55,
                Description = "Originalet.",
                ImageURL = "https://images.unsplash.com/photo-1554866585-cd94860890b7?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=465&q=80"
            });

            database.Items.Add(new Item
            {
                Category = "Dryck",
                Product = "Red Bull",
                Price = 17,
                Stock = 0,
                Description = "Ger dig vingar.",
                ImageURL = "https://images.unsplash.com/photo-1580859297753-0b52fa0fc46e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=386&q=80
            });

            database.Items.Add(new Item
            {
                Category = "Dryck",
                Product = "Mountain Dew",
                Price = 11,
                Stock = 22,
                Description = "Sött vatten.",
                ImageURL = "https://images.unsplash.com/photo-1632134547266-ab2cb69602a1?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1032&q=80"
            });

            database.SaveChanges();
        }
    }
}

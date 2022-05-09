using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hakimslivs.Data;
using hakimslivs.Models;

namespace hakimslivs.Pages.Admin.ProductManager
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public EditModel(ApplicationDbContext database)
        {
            _database = database;
        }

        public Item Item { get; set; }
        [BindProperty]
        public int Kronor { get; set; }
        [BindProperty]
        public int Öre { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public async Task LoadCategories()
        {
            Categories = await _database.Categories.Select(p => new SelectListItem
            {
                Value = p.Name,
                Text = p.Name
            }).Distinct()
                .ToListAsync();

            SelectListItem none = new SelectListItem
            {
                Value = "Ingen",
                Text = "Ingen"
            };
            Categories.Insert(0, none);
        }

        public async Task LoadItem(int id)
        {
            Item = await _database.Items.Include(i => i.Category).FirstOrDefaultAsync(m => m.ID == id);
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadCategories();
            await LoadItem(id);

            
            var price = Item.Price.ToString().Split(",");
            Kronor = int.Parse(price[0]);
            Öre = int.Parse(price[1]);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, Item item)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            await LoadCategories();
            await LoadItem(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string price = Kronor + "." + Öre;

            Item.Product = item.Product;
            Item.Description = item.Description;
            Item.Stock = item.Stock;
            Item.ImageURL = item.ImageURL;
            try
            {
                Item.Price = decimal.Parse(price);
            }
            catch
            {
                return Page();
            }

            try
            {
                Item.Category = await _database.Categories.FirstAsync(c => c.Name == item.Category.Name);
            }
            catch
            {
                Item.Category = null;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemExists(int id)
        {
            return _database.Items.Any(e => e.ID == id);
        }
    }
}

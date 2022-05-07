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

        [BindProperty]
        public Item Item { get; set; }
        [BindProperty]
        public int Kronor { get; set; }
        [BindProperty]
        public int Öre { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public async Task LoadCategories()
        {
            Categories = await _database.Items.Select(p => new SelectListItem
            {
                Value = p.Category.ToString(),
                Text = p.Category.ToString()
            }).Distinct()
                .ToListAsync();

            SelectListItem none = new SelectListItem
            {
                Value = "Ingen",
                Text = "Ingen"
            };
            Categories.Insert(0, none);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await LoadCategories();
            if (id == null)
            {
                return NotFound();
            }

            Item = await _database.Items.Include(i => i.Category).FirstOrDefaultAsync(m => m.ID == id);
            var price = Item.Price.ToString().Split(",");
            Kronor = int.Parse(price[0]);
            Öre = int.Parse(price[1]);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            await LoadCategories();
            string price = Kronor + "." + Öre;
            try
            {
                Item.Price = decimal.Parse(price);
            }
            catch
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _database.Attach(Item).State = EntityState.Modified;

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

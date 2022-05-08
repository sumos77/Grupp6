using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using hakimslivs.Data;
using hakimslivs.Models;
using Microsoft.EntityFrameworkCore;

namespace hakimslivs.Pages.Admin.ProductManager
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public CreateModel(ApplicationDbContext database)
        {
            _database = database;
        }
        public List<SelectListItem> Categories { get; set; }
        [BindProperty]
        public int Kronor { get; set; }
        [BindProperty]
        public int Öre { get; set; }
        [BindProperty]
        public Item Item { get; set; }

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
        public async Task<IActionResult> OnGet()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            await LoadCategories();
            return Page();
        } 

        public async Task<IActionResult> OnPostAsync(Item item)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            await LoadCategories();
            Item.Price = 0;
            
            if (!ModelState.IsValid)
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
            
            string price = Kronor + "." + Öre;
            try
            {
                Item.Price = decimal.Parse(price);
            }
            catch
            {
                return Page();
            }
            
            _database.Items.Add(Item);
            await _database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

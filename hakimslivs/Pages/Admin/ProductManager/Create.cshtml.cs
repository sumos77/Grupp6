using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> OnGet()
        {
            await LoadCategories();
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            await LoadCategories();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _database.Items.Add(Item);
            await _database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

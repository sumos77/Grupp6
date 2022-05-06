using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimslivs.Data;
using hakimslivs.Models;

namespace hakimslivs.Pages
{
    public class IndexModel : PageModel
    {
        private readonly hakimslivs.Data.ApplicationDbContext _context;

        public IndexModel(hakimslivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }

        public async Task OnGetAsync()
        {
            Item = await _context.Items.Include(i => i.Category).ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;

        public IndexModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context)
        {
            _context = context;
        }

        // This is the list of all fights that have happened, ordered by most recent first.
        public IList<FightHistory> FightHistory { get; set; } = default!;

        public async Task OnGetAsync()
        {
            FightHistory = await _context.FightHistory.ToListAsync();
        }
    }
}

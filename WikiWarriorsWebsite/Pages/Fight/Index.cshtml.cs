using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Pages.Fight
{
    public class IndexModel : PageModel
    {
        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;
        
        // Access URL variables so that we can recieve the selected fighter names.
        [BindProperty(SupportsGet = true)]
        public string Fighter1 { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Fighter2 { get; set; }

        public IndexModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context)
        {
            _context = context;
        }

        public IList<FightHistory> FightHistory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Fighter names can be accessed through ViewData
            ViewData["fighter1"] = Fighter1;
            ViewData["fighter2"] = Fighter2;

            // Get fighters images
            ViewData["fighter1Image"] = "https://upload.wikimedia.org/wikipedia/commons/7/7d/Arch_of_Titus_Menorah.png";
            ViewData["fighter2Image"] = "https://upload.wikimedia.org/wikipedia/commons/f/fe/Manchester_Central_Library.jpg";

            FightHistory = await _context.FightHistory
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter2)
                .Include(f => f.Winner).ToListAsync();
        }
    }
}

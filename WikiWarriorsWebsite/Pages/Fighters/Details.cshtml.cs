using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Pages.Fighters
{
    public class DetailsModel : PageModel
    {
        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;

        public DetailsModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context)
        {
            _context = context;
        }

        public Fighter Fighter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fighter = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == id);

            if (fighter is not null)
            {
                Fighter = fighter;

                return Page();
            }

            return NotFound();
        }
    }
}

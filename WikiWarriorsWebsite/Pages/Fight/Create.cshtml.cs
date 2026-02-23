using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Pages.Fight
{
    public class CreateModel : PageModel
    {
        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;

        public CreateModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
        ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
        ViewData["WinnerId"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
            return Page();
        }

        [BindProperty]
        public FightHistory FightHistory { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FightHistory.Add(FightHistory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

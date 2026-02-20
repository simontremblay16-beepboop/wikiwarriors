using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Pages.Fight
{
    public class EditModel : PageModel
    {
        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;

        public EditModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FightHistory FightHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fighthistory =  await _context.FightHistory.FirstOrDefaultAsync(m => m.FightHistoryId == id);
            if (fighthistory == null)
            {
                return NotFound();
            }
            FightHistory = fighthistory;
           ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
           ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
           ViewData["WinnerId"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FightHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FightHistoryExists(FightHistory.FightHistoryId))
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

        private bool FightHistoryExists(int id)
        {
            return _context.FightHistory.Any(e => e.FightHistoryId == id);
        }
    }
}

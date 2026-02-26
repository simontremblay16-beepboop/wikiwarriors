using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //public Fighter Fighter1Record { get; set; } = default!;

        [BindProperty]
        public FightHistory NewFightRecord { get; set; } = default!;

        public SelectList AllFighters { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            AllFighters = new SelectList(_context.Fighter, "FighterId", "FighterId");
            //ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
            //ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
            //ViewData["WinnerId"] = new SelectList(_context.Fighter, "FighterId", "FighterId");
            //return Page();
            // Fighter ids can be accessed through ViewData
            int fighter1Id = int.Parse(Fighter1);
            int fighter2Id = int.Parse(Fighter2);
            ViewData["fighter1Id"] = fighter1Id;
            ViewData["fighter2Id"] = fighter2Id;

            // Get database entries for the fighter
            var fighter1Record = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == fighter1Id);
            var fighter2Record = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == fighter2Id);

            if (fighter1Record is not null && fighter2Record is not null)
            {
                
                // Get fighters data
                ViewData["fighter1Name"] = fighter1Record.Name;
                ViewData["fighter1ImageUrl"] = fighter1Record.ImageUrl;
                ViewData["fighter1PageUrl"] = fighter1Record.PageUrl;
                ViewData["fighter1WordCount"] = fighter1Record.WordCount;
                ViewData["fighter1ReferenceCount"] = fighter1Record.ReferenceCount;
                ViewData["fighter1LinkCount"] = fighter1Record.LinkCount;

                ViewData["fighter2Name"] = fighter2Record.Name;
                ViewData["fighter2ImageUrl"] = fighter2Record.ImageUrl;
                ViewData["fighter2PageUrl"] = fighter2Record.PageUrl;
                ViewData["fighter2WordCount"] = fighter2Record.WordCount;
                ViewData["fighter2ReferenceCount"] = fighter2Record.ReferenceCount;
                ViewData["fighter2LinkCount"] = fighter2Record.LinkCount;

                return Page();
            }

            return NotFound();

            /*
            FightHistory = await _context.FightHistory
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter2)
                .Include(f => f.Winner).ToListAsync();*/
        }

        // Once the fight has concluded, make a POST request to add the fight record to the database
        public async Task<IActionResult> OnPostAsync()
        {
            //NewFightRecord.Fighter1 = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == NewFightRecord.Fighter1Id);
            //NewFightRecord.Fighter2 = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == NewFightRecord.Fighter2Id);
            //NewFightRecord.Winner = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == NewFightRecord.WinnerId);
            if (!ModelState.IsValid)
            {

                return Page();
            }

            _context.FightHistory.Add(NewFightRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}

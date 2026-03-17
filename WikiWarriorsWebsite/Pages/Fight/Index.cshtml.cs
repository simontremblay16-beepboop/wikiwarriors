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

        // Access URL variables so that we can recieve the winner an loser for the victory popup
        [BindProperty(SupportsGet = true)]
        public string? Winner { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Loser { get; set; }
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
            // This code is only used if the page is called with
            // Url variables indicating that a "Victory" popup
            // window is required.
            if (Winner != null && Loser != null)
            {
                int winnerId = int.Parse(Winner);
                int loserId = int.Parse(Loser);

                // Get database entries for the winner and loser fighters
                var winnerRecord = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == winnerId);
                var loserRecord = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == loserId);

                // Update view data so that the popup knows what to display
                ViewData["winnerName"] = winnerRecord.Name;
                ViewData["loserName"] = loserRecord.Name;
                ViewData["winnerImageUrl"] = winnerRecord.ImageUrl;
                ViewData["popupDisplay"] = "block";
            }
            else
            {
                ViewData["popupDisplay"] = "none";
            }

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
            Fighter fighter1Record = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == fighter1Id);
            Fighter fighter2Record = await _context.Fighter.FirstOrDefaultAsync(m => m.FighterId == fighter2Id);

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

                // Collect info from database
                Fighter Fighter1 = fighter1Record;
                Fighter Fighter2 = fighter2Record;

                // Calculate the winner
                // Temporary fight victory equasion
                int winnerId;
                int loserId;
                int fighter1Score = Fighter1.WordCount + Fighter1.ReferenceCount + Fighter1.LinkCount;
                int fighter2Score = Fighter2.WordCount + Fighter2.ReferenceCount + Fighter2.LinkCount;
                if (fighter1Score > fighter2Score)
                {
                    winnerId = Fighter1.FighterId;
                    loserId = Fighter2.FighterId;
                }
                else
                {
                    winnerId = Fighter2.FighterId;
                    loserId = Fighter1.FighterId;
                }

                // Add winner and loser to ViewData so that javascript can access it
                ViewData["winnerId"] = winnerId;
                ViewData["loserId"] = loserId;


                return Page();
            }

            return NotFound();

            /*
            FightHistory = await _context.FightHistory
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter2)
                .Include(f => f.Winner).ToListAsync();*/
        }

        /* The following code no longer used, we decided to move database updates to the selection page

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

            // We must also calculate who the loser is so that we can
            // indicate them on the Victory popup
            int? winnerId = NewFightRecord.WinnerId;
            int? loserId;
            if (NewFightRecord.WinnerId == NewFightRecord.Fighter1Id)
            {
                loserId = NewFightRecord.Fighter2Id;
            }
            else
            {
                loserId = NewFightRecord.Fighter1Id;
            }

            // When we redirect to the index page, we add url variables
            // for the winner and loser so that the index can bring up
            // the "Victory" popup.
            return RedirectToPage("/Index", new
            {
                winner = winnerId.ToString(),
                loser = loserId.ToString()
            });
        }

        */
    }
}

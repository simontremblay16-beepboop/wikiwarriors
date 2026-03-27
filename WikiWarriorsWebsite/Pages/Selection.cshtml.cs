using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;


namespace WikiWarriorsWebsite.Pages
{
    public class SelectionModel : PageModel
    {

        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;

        private readonly SearchService _searcher;

        public SelectionModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context, SearchService searcher)
        {
            _context = context;
            _searcher = searcher;
        }
        [BindProperty]
        public string? Search { get; set; }
        [BindProperty]
        public int? PageId { get; set; }
        [BindProperty]
        public int Fighter1Id { get; set; }
        [BindProperty]
        public int Fighter2Id { get; set; }
        public List<ResultStruct>? Results { get; set; }

        public async Task<IActionResult> OnPostSearch()
        {
            Results = await _searcher.Search(Search);

            return Page();
        }

        public async Task<IActionResult> OnPostFight()
        {
            // Add fight to databasee
            FightHistory NewFightRecord = new FightHistory();

            // Call dataloader, to create the fighters and 
            // save the fighters to the database
            await _searcher.SaveSelectedArticleInfoAsync(Fighter1Id);
            await _searcher.SaveSelectedArticleInfoAsync(Fighter2Id);

            Fighter Fighter1 = _context.Fighter.FirstOrDefault(m => m.FighterId == Fighter1Id);
            Fighter Fighter2 = _context.Fighter.FirstOrDefault(m => m.FighterId == Fighter2Id);

            // Calculate winner
            // Temporary fight victory equasion
            int winnerId;
            int fighter1Score = (Fighter1.LinkCount * Fighter1.ReferenceCount) + Fighter1.WordCount;
            int fighter2Score = (Fighter2.LinkCount * Fighter2.ReferenceCount) + Fighter2.WordCount;
            if (fighter1Score > fighter2Score)
            {
                winnerId = Fighter1.FighterId;
            }
            else
            {
                winnerId = Fighter2.FighterId;
            }

            // Update database with the new fight
            NewFightRecord.Fighter1Id = Fighter1.FighterId;
            NewFightRecord.Fighter2Id = Fighter2.FighterId;
            NewFightRecord.WinnerId = winnerId;
            NewFightRecord.FightDate = DateTime.Now;
            NewFightRecord.DailyFight = false;
            NewFightRecord.Fighter1 = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.Fighter1Id);
            NewFightRecord.Fighter2 = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.Fighter2Id);
            NewFightRecord.Winner = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.WinnerId);
            if (ModelState.IsValid)
            {
                _context.FightHistory.Add(NewFightRecord);
                _context.SaveChanges();
            }

            return RedirectToPage("/Fight/Index", new { fighter1 = Fighter1Id.ToString(), fighter2 = Fighter2Id.ToString() });//return Page();
        }
    }
}

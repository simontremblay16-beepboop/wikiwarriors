using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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

        // Access URL variables so that we can recieve the winner an loser for the victory popup
        [BindProperty(SupportsGet = true)]
        public string? Winner { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Loser { get; set; }
        // URL variable for if we need to create a new daily fight
        [BindProperty(SupportsGet = true)]
        public string? CreateDaily { get; set; }

        // Fight history list object
        public IList<FightHistory> FightHistory { get; set; } = default!;

        public void OnGet()
        {
            // This code is only used if the page is called with
            // Url variables indicating that a "Victory" popup
            // window is required.
            if (Winner != null && Loser != null)
            {
                int winnerId = int.Parse(Winner);
                int loserId = int.Parse(Loser);

                // Get database entries for the winner and loser fighters
                var winnerRecord = _context.Fighter.FirstOrDefault(m => m.FighterId == winnerId);
                var loserRecord = _context.Fighter.FirstOrDefault(m => m.FighterId == loserId);

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

            // This code will run if the CreateDaily url variable is set, indicating that its a new day and we must make a new daily fight
            if (CreateDaily != null)
            {
                FightHistory NewFightRecord = new FightHistory();
                // Get the fighter Ids for todays fight
                string firstWikiURL = "https://en.wikipedia.org/wiki/Westminster_Cathedral"; // Get from API
                string secondWikiURL = "https://en.wikipedia.org/wiki/Wikipedia"; // Get from API

                // *** Do something to call dataloader, passing in to URLs so that the items are added to the database ***

                // Collect info from database
                Fighter Fighter1 = _context.Fighter.FirstOrDefault(m => m.PageUrl == firstWikiURL);
                Fighter Fighter2 = _context.Fighter.FirstOrDefault(m => m.PageUrl == secondWikiURL);

                // Calculate the winner
                // Temporary fight victory equasion
                int winnerId;
                int fighter1Score = Fighter1.WordCount + Fighter1.ReferenceCount + Fighter1.LinkCount;
                int fighter2Score = Fighter2.WordCount + Fighter2.ReferenceCount + Fighter2.LinkCount;
                if (fighter1Score > fighter2Score)
                {
                    winnerId = Fighter1.FighterId;
                }
                else
                {
                    winnerId = Fighter2.FighterId;
                }

                // Update database with the daily fight
                NewFightRecord.Fighter1Id = Fighter1.FighterId;
                NewFightRecord.Fighter2Id = Fighter2.FighterId;
                NewFightRecord.WinnerId = winnerId;
                NewFightRecord.FightDate = DateTime.Now;
                NewFightRecord.DailyFight = true;
                NewFightRecord.Fighter1 = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.Fighter1Id);
                NewFightRecord.Fighter2 = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.Fighter2Id);
                NewFightRecord.Winner = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.WinnerId);
                if (ModelState.IsValid)
                {
                    _context.FightHistory.Add(NewFightRecord);
                    _context.SaveChanges();
                }
            }

            // Load in FightHistory Table
            FightHistory = _context.FightHistory
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter2)
                .Include(f => f.Winner).ToList();

            // Select only daily fights
            int index = FightHistory.Count - 1;
            // You might be wondering why this loop to find the daily fight is so strange;
            // Wanted to minimize the number of times the code interacts with the database
            // (to save Chris' Azure credits), so this loop runs backward in order to find
            // the lastest fight first, and exits as soon as the daily fight is found.
            Fighter DailyFightsWinner = null;
            Fighter DailyFightsLoser = null;
            DateTime DailyFightsDate = DateTime.Now;
            int DailyFightsId;
            while (index >= 0)
            {
                if (FightHistory[index].DailyFight)
                {

                    var CurrentWinner = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[index].WinnerId);

                    // This code for calculating the loser isn't pretty, but its the only way to avoid the compiler
                    // complaining that CurrentLoser isn't initialised.
                    Fighter CurrentLoser;
                    if (FightHistory[index].WinnerId == FightHistory[index].Fighter1Id)
                    {
                        CurrentLoser = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[index].Fighter2Id);
                    }
                    else {
                        CurrentLoser = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[index].Fighter1Id);
                    }

                    DailyFightsWinner = CurrentWinner;
                    DailyFightsLoser = CurrentLoser;
                    DailyFightsDate = FightHistory[index].FightDate;
                    DailyFightsId = index;
                    index = -1;
                }
                index --;
            }

            /*
            // Set dailyFightNum to be total daily fights, since we are always show the most
            // Recent daily fight
            ViewData["dailyFightNum"] = DailyFightsDates.Count;

            // Now find the index of the most recent fight
            int mostRecentIndex = 0;
            DateTime mostRecentDate = DailyFightsDates[0];
            for (int i = 0; i < DailyFightsDates.Count; i++)
            {
                if (DailyFightsDates[i] > mostRecentDate)
                {
                    mostRecentDate = DailyFightsDates[i];
                    mostRecentIndex = i;
                }
            }*/

            // And set "Today"'s daily fight based on that mostRecentIndex
            //int? currentFighter1Id = FightHistory[DailyFightsIds[mostRecentIndex]].Fighter1Id;
            //int? currentFighter2Id = FightHistory[DailyFightsIds[mostRecentIndex]].Fighter2Id;
            ViewData["dailyFightFighter1Name"] = DailyFightsWinner.Name;
            ViewData["dailyFightFighter2Name"] = DailyFightsLoser.Name;
            ViewData["dailyFightWinnerName"] = DailyFightsWinner.Name;
            string year = DailyFightsDate.Year.ToString();
            string month = DailyFightsDate.Month.ToString();
            string day = DailyFightsDate.Day.ToString();
            if (year.Length < 2) {
                year = "0" + year;
            }
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            if (day.Length < 2)
            {
                day = "0" + day;
            }
            string parsedDate = year + "-" + month + "-" + day;
            ViewData["dailyFightDate"] = parsedDate;
            ViewData["dailyFightFighter1ImageUrl"] = DailyFightsWinner.ImageUrl;
            ViewData["dailyFightFighter2ImageUrl"] = DailyFightsLoser.ImageUrl;
        
        }


    }
}

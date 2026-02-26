using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        // Fight history list object
        public IList<FightHistory> FightHistory { get; set; } = default!;

        public IList<Fighter> DailyFightsWinners { get; set; } = [];
        public IList<Fighter> DailyFightsLosers { get; set; } = [];
        public IList<DateTime> DailyFightsDates { get; set; } = [];
        public IList<int> DailyFightsIds { get; set; } = [];
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

            // Load in FightHistory Table
            FightHistory = _context.FightHistory.ToList();

            // Select only daily fights
            for (int i = 0; i < FightHistory.Count; i++)
            {
                if (FightHistory[i].DailyFight)
                {

                    var CurrentWinner = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[i].WinnerId);

                    // This code for calculating the loser isn't pretty, but its the only way to avoid the compiler
                    // complaining that CurrentLoser isn't initialised.
                    var CurrentLoser = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[i].Fighter1Id);
                    if (FightHistory[i].WinnerId == FightHistory[i].Fighter1Id)
                    {
                        CurrentLoser = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[i].Fighter2Id);
                    }

                    DailyFightsWinners.Add(CurrentWinner);
                    DailyFightsLosers.Add(CurrentLoser);
                    DailyFightsDates.Add(FightHistory[i].FightDate);
                    DailyFightsIds.Add(i);

                }
            }

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
            }

            // And set "Today"'s daily fight based on that mostRecentIndex
            int? currentFighter1Id = FightHistory[DailyFightsIds[mostRecentIndex]].Fighter1Id;
            int? currentFighter2Id = FightHistory[DailyFightsIds[mostRecentIndex]].Fighter2Id;
            ViewData["dailyFightFighter1Name"] = _context.Fighter.FirstOrDefault(m => m.FighterId == currentFighter1Id).Name;
            ViewData["dailyFightFighter2Name"] = _context.Fighter.FirstOrDefault(m => m.FighterId == currentFighter2Id).Name;
            ViewData["dailyFightWinnerName"] = DailyFightsWinners[mostRecentIndex].Name;
            ViewData["dailyFightDate"] = mostRecentDate;
            ViewData["dailyFightFighter1ImageUrl"] = _context.Fighter.FirstOrDefault(m => m.FighterId == currentFighter1Id).ImageUrl;
            ViewData["dailyFightFighter2ImageUrl"] = _context.Fighter.FirstOrDefault(m => m.FighterId == currentFighter2Id).ImageUrl;
        }
    }
}

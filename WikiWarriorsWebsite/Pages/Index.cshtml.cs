using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;

namespace WikiWarriorsWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;

        private readonly SearchService _searcher;

        public IndexModel(WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context, SearchService searcher)
        {
            _context = context;
            _searcher = searcher;
        }

        //// Access URL variables so that we can recieve the winner an loser for the victory popup
        //[BindProperty(SupportsGet = true)]
        //public string? Winner { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? Loser { get; set; }
        // URL variable for if we need to create a new daily fight
        //[BindProperty(SupportsGet = true)]
        //public string? CreateDaily { get; set; }

        // Fight history list object
        public IList<FightHistory> FightHistory { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            DateTime currentDate = DateTime.UtcNow;
            int currentYear = currentDate.Year;
            int currentMonth = currentDate.Month;
            int currentDay = currentDate.Day;

            string currentYearStr = currentYear.ToString();
            if (currentYearStr.Length < 4)
            {
                currentYearStr = "0" + currentYearStr;
            }
            string currentMonthStr = currentMonth.ToString();
            if (currentMonthStr.Length < 2)
            {
                currentMonthStr = "0" + currentMonthStr;
            }
            string currentDayStr = currentDay.ToString();
            if (currentDayStr.Length < 2)
            {
                currentDayStr = "0" + currentDayStr;
            }


            /*
            int featuredArticle = await _searcher.GetFeaturedArticle(currentYearStr, currentMonthStr, currentDayStr);

            ViewData["featuredArticle"] = featuredArticle;

            int inTheNews = await _searcher.GetInTheNews(currentYearStr, currentMonthStr, currentDayStr);

            ViewData["inTheNews"] = inTheNews;
            */

            //// This code is only used if the page is called with
            //// Url variables indicating that a "Victory" popup
            //// window is required.
            //if (Winner != null && Loser != null)
            //{
            //    int winnerId = int.Parse(Winner);
            //    int loserId = int.Parse(Loser);

            //    // Get database entries for the winner and loser fighters
            //    var winnerRecord = _context.Fighter.FirstOrDefault(m => m.FighterId == winnerId);
            //    var loserRecord = _context.Fighter.FirstOrDefault(m => m.FighterId == loserId);

            //    // Update view data so that the popup knows what to display
            //    ViewData["winnerName"] = winnerRecord.Name;
            //    ViewData["loserName"] = loserRecord.Name;
            //    ViewData["winnerImageUrl"] = winnerRecord.ImageUrl;
            //    ViewData["popupDisplay"] = "block";
            //}
            //else
            //{
            //    ViewData["popupDisplay"] = "none";
            //}

            string currentDateStr = currentYearStr + "-" + currentMonthStr + "-" + currentDayStr;

            string lastDailyStr = GetDailyFights();

            // This code will run if the CreateDaily url variable is set, indicating that its a new day and we must make a new daily fight
            if (currentDateStr != lastDailyStr)
            {
                int featuredArticle = await _searcher.GetFeaturedArticle(currentYearStr, currentMonthStr, currentDayStr);

                ViewData["featuredArticle"] = featuredArticle;

                int inTheNews = await _searcher.GetInTheNews(currentYearStr, currentMonthStr, currentDayStr);

                ViewData["inTheNews"] = inTheNews;

                // Add daily fight
                FightHistory NewFightRecord = new FightHistory();

                // Call dataloader, to create the fighters
                await _searcher.SaveSelectedArticleInfoAsync(featuredArticle);
                await _searcher.SaveSelectedArticleInfoAsync(inTheNews);

                Fighter Fighter1 = _context.Fighter.FirstOrDefault(m => m.FighterId == featuredArticle);
                Fighter Fighter2 = _context.Fighter.FirstOrDefault(m => m.FighterId == inTheNews);

                // Calculate the winner
                // Fight equation 
                int winnerId;
                int fighter1Score = (Fighter1.LinkCount * Fighter1.ReferenceCount) + Fighter1.WordCount;
                int fighter2Score = (Fighter2.LinkCount * Fighter2.ReferenceCount) + Fighter2.WordCount;
                if (fighter1Score >= fighter2Score)
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
                NewFightRecord.FightDate = currentDate;//DateTime.Now;
                NewFightRecord.DailyFight = true;
                NewFightRecord.Fighter1 = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.Fighter1Id);
                NewFightRecord.Fighter2 = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.Fighter2Id);
                NewFightRecord.Winner = _context.Fighter.FirstOrDefault(m => m.FighterId == NewFightRecord.WinnerId);
                if (ModelState.IsValid)
                {
                    _context.FightHistory.Add(NewFightRecord);
                    _context.SaveChanges();
                }

                // Recalculate daily fights now that one more is added.
                lastDailyStr = GetDailyFights();
            }

            return Page();
        }

        public string GetDailyFights() {
            // Default value for parsed date
            // Meaning no prior daily fight
            string parsedDate = "";

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
            DateTime DailyFightsDate = DateTime.UtcNow;
            int DailyFightsId = 0;
            while (index > -1)
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
                    else
                    {
                        CurrentLoser = _context.Fighter.FirstOrDefault(m => m.FighterId == FightHistory[index].Fighter1Id);
                    }

                    DailyFightsWinner = CurrentWinner;
                    DailyFightsLoser = CurrentLoser;
                    DailyFightsDate = FightHistory[index].FightDate;
                    DailyFightsId = FightHistory[index].FightHistoryId;
                    index = -1;
                }
                index--;
            }

            // Check if daily fight winner has even been set. If not, that means
            // we haven't ever had a daily fight
            if (DailyFightsWinner != null)
            {
                ViewData["dailyFightFighter1Name"] = DailyFightsWinner.Name;
                ViewData["dailyFightFighter2Name"] = DailyFightsLoser.Name;
                ViewData["dailyFightWinnerName"] = DailyFightsWinner.Name;
                ViewData["dailyFighter1Url"] = DailyFightsWinner.PageUrl;
                ViewData["dailyFighter2Url"] = DailyFightsLoser.PageUrl;
                string year = DailyFightsDate.Year.ToString();
                string month = DailyFightsDate.Month.ToString();
                string day = DailyFightsDate.Day.ToString();
                if (year.Length < 4)
                {
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
                parsedDate = year + "-" + month + "-" + day;
                ViewData["dailyFightDate"] = parsedDate;
                ViewData["dailyFightFighter1ImageUrl"] = DailyFightsWinner.ImageUrl;
                ViewData["dailyFightFighter2ImageUrl"] = DailyFightsLoser.ImageUrl;
                ViewData["dailyFightsId"] = DailyFightsId;
            }
            return parsedDate;
        }
    }
}

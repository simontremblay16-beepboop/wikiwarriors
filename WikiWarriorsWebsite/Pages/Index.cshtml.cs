using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}

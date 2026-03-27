using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;   
using WikiWarriorsWebsite.Data;
using WikiWarriorsWebsite.Models;


namespace WikiWarriorsWebsite.Pages
{
    public class SelectionModel : PageModel
    {
       
        private readonly SearchService _searcher;

        public SelectionModel(SearchService searcher) { _searcher = searcher;}
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

            await _searcher.SaveSelectedArticleInfoAsync(Fighter1Id);
            await _searcher.SaveSelectedArticleInfoAsync(Fighter2Id);

            return Page();
        }
    }
}

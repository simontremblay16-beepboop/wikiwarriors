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
        public string Name { get; set; }

        public List<string> Results { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            Results = await _searcher.Search(Name);


            if (ModelState.IsValid) {
                Console.WriteLine("WORKS");
            }
            
            Console.WriteLine("done");
           
            Console.WriteLine("...");

            return Page();
        }
    }
}

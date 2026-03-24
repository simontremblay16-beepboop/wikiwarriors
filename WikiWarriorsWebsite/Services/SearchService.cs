using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WikiWarriorsWebsite.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class SearchService
{
    //this is the service that will actually do the searching. I moved it to its own file so it can be used anywhere.

        // hey I just delcared you,
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    private readonly WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext _context;


    //and this constructor is crazy,
    public SearchService(IConfiguration config, HttpClient httpClient, WikiWarriorsWebsite.Data.WikiWarriorsWebsiteContext context)
    {
        //but here's my config and httpclient,
        _config = config;
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<bool> DoesDatabaseHaveItAsync(int id) 
    {
       return _context.Fighter.Any(x =>x.FighterId == id);
    }

    public async Task<int> SaveSelectedArticleInfoAsync(int PageId)
    {
       

        //check fighter DB if result exists.

        //if yes load that and return 1
        if (await DoesDatabaseHaveItAsync(PageId)) 
        {
            
            return 1;
        }
        //else create fighter below and return 2
        else {
            //word count??
            //test pageId: 18978754
            string wikiUrl = $"https://en.wikipedia.org/w/api.php?action=query&pageids={PageId}&format=json&prop=extlinks|info|extracts|links|pageimages&ellimit=max&inprop=url&pllimit=max&explaintext&piprop=original";

            string json = await _httpClient.GetStringAsync(wikiUrl);
            SResultRoot? resultsObj = JsonConvert.DeserializeObject<SResultRoot>(json);
            
            Fighter newFighter = new Fighter();
            newFighter.FighterId = resultsObj.query.Info._id;
            newFighter.Name = resultsObj.query.Info._Name;
            newFighter.LinkCount = resultsObj.query.Info._Links;
            newFighter.ReferenceCount = resultsObj.query.Info._References;
            newFighter.WordCount = resultsObj.query.Info._Wordcount;
            newFighter.PageUrl = resultsObj.query.Info._ArticleUrl;
            newFighter.ImageUrl = resultsObj.query.Info._ImageUrl;
            return 2;
        }


    }
    public async Task<List<ResultStruct>> Search(string name)
    {
        var results = await SearchWikipedia(name);
        return results;
    }

    private async Task<List<ResultStruct>> SearchWikipedia(string name)
    {
        string wikiUrl =
            $"https://en.wikipedia.org/w/api.php?action=query&generator=search&gsrsearch={name}&gsrlimit=5&format=json&titles={name}&prop=info|pageimages|description&inprop=url&piprop=original";

              
      
        //get json from api query
        string json = await _httpClient.GetStringAsync(wikiUrl);
        // read raw JSON and deserialize 
        ResultRoot? resultsObj = JsonConvert.DeserializeObject<ResultRoot>(json);

        var resultsList = new List<ResultStruct>();
        // check if the results are valid
        if (resultsObj?.query?.pages != null)
        {
            foreach (var page in resultsObj.query.pages.Values)
            {
                resultsList.Add(new ResultStruct
                {
                    PageId = page.PageId,
                    Title = page.Title,
                    Description = page.Description,
                    ArticleUrl = page.ArticleUrl,
                    ImageUrl = page.ImageUrl
                });
            }
        }

        return resultsList;
    }

}
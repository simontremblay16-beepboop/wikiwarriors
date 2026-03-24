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
        else
        {
            //word count??
            //test pageId: 18978754
            string wikiUrl = $"https://en.wikipedia.org/w/api.php?action=query&pageids={PageId}&format=json&prop=extlinks|info|extracts|links|pageimages&ellimit=max&inprop=url&pllimit=max&explaintext&piprop=original";

            string json = await _httpClient.GetStringAsync(wikiUrl);

            SResultRoot? resultsObj = JsonConvert.DeserializeObject<SResultRoot>(json);
            Fighter newFighter = new Fighter();

            foreach (var item in resultsObj.query.Info) 
            {
                item.Value.doMagic();
                newFighter.FighterId = item.Value._id;
                newFighter.Name = item.Value._Name;
                newFighter.LinkCount = item.Value._Links;
                newFighter.ReferenceCount = item.Value._References;
                newFighter.WordCount = item.Value._Wordcount;
                newFighter.PageUrl = item.Value._ArticleUrl;

                // Sometimes there is no image url
                newFighter.ImageUrl = item.Value._ImageUrl;
                if (newFighter.ImageUrl == null)
                {
                    // If no image is found, use backup image api
                    try
                    {
                        string backupImageUrl = "https://en.wikipedia.org/w/api.php?action=query&prop=pageimages&format=json&piprop=name&titles=" + newFighter.Name;
                        string backupJson = await _httpClient.GetStringAsync(backupImageUrl);
                        dynamic backupObj = JsonConvert.DeserializeObject(backupJson);
                        newFighter.ImageUrl = (string)backupObj.pageimage;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    // If we STILL have no image url
                    if (newFighter.ImageUrl == null)
                    {
                        newFighter.ImageUrl = "image not found";
                    }

                }
  
                _context.Database.OpenConnection();

                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Fighter ON");
                    _context.Fighter.Add(newFighter);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Fighter OFF");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                       
                    _context.Database.CloseConnection();
                }
            }
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
            $"https://en.wikipedia.org/w/api.php?action=query&generator=search&gsrsearch={name}&gsrlimit=8&format=json&titles={name}&prop=info|pageimages|description&inprop=url&piprop=original";



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

    public async Task<int> GetFeaturedArticle(string y, string m, string d)
    {
        int result = 0;

        string wikiUrl = "https://api.wikimedia.org/feed/v1/wikipedia/en/featured/" + y + "/" + m + "/" + d;

        string json = await _httpClient.GetStringAsync(wikiUrl);

        dynamic obj = JsonConvert.DeserializeObject(json);
        //Console.WriteLine(obj.tfa.pageid);

        result = int.Parse(((string)obj.tfa.pageid));

        return result;
    }

    public async Task<int> GetInTheNews(string y, string m, string d)
    {
        int result = 0;

        string wikiUrl = "https://api.wikimedia.org/feed/v1/wikipedia/en/featured/" + y + "/" + m + "/" + d;

        string json = await _httpClient.GetStringAsync(wikiUrl);

        dynamic obj = JsonConvert.DeserializeObject(json);
        //Console.WriteLine(obj.tfa.pageid);

        result = int.Parse(((string)obj.news[0].links[0].pageid));

        return result;
    }
}
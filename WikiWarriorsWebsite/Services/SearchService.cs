using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using WikiWarriorsWebsite.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class SearchService
{
    //this is the service that will actually do the searching. I moved it to its own file so it can be used anywhere.

        // hey I just delcared you,
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

        //and this constructor is crazy,
    public SearchService(IConfiguration config, HttpClient httpClient)
    {
        //but here's my config and httpclient,
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<int> SaveSelectedArticleInfoAsync(int PageId)
    {
        int result = 0;

        //check fighter DB if result exists.

        //if yes load that and return 1
        if (result == 1000) //fake code
        {
            result = 1;
            //code goes here
            return result;
        }
        //else create fighter below and return 2
        else {
            //word count??
            //test pageId: 18978754
            string wikiUrl = $"https://en.wikipedia.org/w/api.php?action=query&pageids={PageId}&format=json&prop=extlinks|extracts|links|pageimages&ellimit=max&pllimit=max&explaintext&piprop=original";

            string json = await _httpClient.GetStringAsync(wikiUrl);
            SResultRoot? resultsObj = JsonConvert.DeserializeObject<SResultRoot>(json);

            foreach (var prop in resultsObj.query.pages.Values) {

            }

            return 2;
        }

        //I need to search the selected page and get its:
        //Thumbnail URL
        //Word count
        //Links
        //References

        //t

        // use "fullurl" in the response to get only the article URL
        //Thumbnail URL (https://en.wikipedia.org/w/api.php?action=query&prop=pageimages&piprop=thumbnail},
        //Links (https://en.wikipedia.org/w/api.php?action=query&),
        //References (https://en.wikipedia.org/w/api.php?action=query& ),
        //and Word Count (https://en.wikipedia.org/w/api.php?action=query& extracts with prop:explaintext)

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
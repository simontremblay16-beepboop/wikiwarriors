using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text.Json;

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

    
    public async Task<List<string>> Search(string name)
    {
        // Currently prefer Wikipedia search. In future we can search local DB first.
        var results = await SearchWikipedia(name);
        return results;
    }
    //search fighters database
    //private async Task<List<string>> SearchDatabase(string name)
    //{
    //    var results = new List<string>();

    //    using SqlConnection conn =
    //        new SqlConnection(_config.GetConnectionString("WikiWarriorsWebsiteContext"));

    //    await conn.OpenAsync();

    //    string query = @"
    //        SELECT Name
    //        FROM dbo.Fighter
    //        WHERE Name LIKE @search
    //        OR SOUNDEX(Name) = SOUNDEX(@name)";

    //    using SqlCommand cmd = new SqlCommand(query, conn);
    //    cmd.Parameters.AddWithValue("@search", "%" + name + "%");
    //    cmd.Parameters.AddWithValue("@name", name);

    //    using SqlDataReader reader = await cmd.ExecuteReaderAsync();

    //    while (await reader.ReadAsync())
    //    {
    //        results.Add(reader.GetString(0));
    //    }

    //    return results;
    //}

    private async Task<List<string>> SearchWikipedia(string name)
    {
        string wikiUrl =
            $"https://en.wikipedia.org/w/api.php?action=opensearch&search={name}&limit=10&namespace=0&format=json";

        //api requests a JSON document containing results from wikipedia query
        JsonDocument response =  await _httpClient.GetFromJsonAsync<JsonDocument>(wikiUrl);

        //the document, cut into bite sized pieces
        var data = JsonSerializer.Deserialize<JsonElement>(response);
        //create an empty array
        var results = new List<string>();

        //for each result from the query add each piece into 
        foreach (var item in data[1].EnumerateArray())
        {
            results.Add(item.GetString());
        }
        //now return the resulting array of results :)
        return results;
    }
}
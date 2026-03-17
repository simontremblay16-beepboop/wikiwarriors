
using System.Linq;
using Newtonsoft.Json;

namespace WikiWarriorsWebsite.Models
{
    // example response
    //query:{
    //"pages": {
    //  "18978754": {
    //    "pageid": 18978754,
    //    "title": "Apple",
    //    "thumbnail": { "source": "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Pink_lady_and_cross_section.jpg/60px-Pink_lady_and_cross_section.jpg"},
    //    "description": "Edible fruit",
    //    "fullurl": "https://en.wikipedia.org/wiki/Apple",
    //    "editurl": "https://en.wikipedia.org/w/index.php?title=Apple&action=edit",
    //    "canonicalurl": "https://en.wikipedia.org/wiki/Apple"
    //    }
    //}
    //}
    public class ResultRoot
    {
        
        [JsonProperty("query")]
        public ResultQuery? query { get; set; }
    }
    public class SResultRoot : ResultRoot
    {
        [JsonProperty("query")]
        public SResultQuery? query { get; set; }
    }

    public class ResultQuery
    {
        [JsonProperty("pages")]
        public Dictionary<string, ResultStruct>? pages { get; set; }
    }
    public class SResultQuery: ResultQuery
    {
        [JsonProperty("pages")]
        public NewFighterInfo Info { get; set; }
        
    }
    public struct ResultStruct 
    {
        [JsonProperty("pageid")]
        public int PageId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("fullurl")]
        public string ArticleUrl { get; set; }
        [JsonProperty("original")]
        public Image ImageUrl { get; set; }
    }
    public class NewFighterInfo
    {
        [JsonProperty("pageid")]
        public int PageId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("extlinks")]
        public ExtLink[]? Extlinks { get; set; }
        [JsonProperty("extract")]
        public string? Extract { get; set; }

        [JsonProperty("links")]
        public Link[]? Links { get; set; }

        [JsonProperty("original")]
        public Image ImageUrl { get; set; }
    }

    public struct ExtLink {
        [JsonProperty("*")]
        public string? Url { get; set; }    
    }
    public struct Link {

        [JsonProperty("title")]
        public string? Title { get; set; }
    }
    public struct Image 
    {
        [JsonProperty("source")]
        public string Source { get; set; }
    }


}

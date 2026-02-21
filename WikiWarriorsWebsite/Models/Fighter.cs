using System.Security;

namespace WikiWarriorsWebsite.Models
{
    public class Fighter
    {
        public int FighterId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string PageUrl { get; set; }
        public int WordCount { get; set; }
        public int ReferenceCount { get; set; }
        public int LinkCount { get; set; }
    }
}

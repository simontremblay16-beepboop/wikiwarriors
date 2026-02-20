
using System.ComponentModel.DataAnnotations;

namespace WikiWarriorsWebsite.Models
{
    public class FightHistory
    {
        public int FightHistoryId { get; set; }
        public int? Fighter1Id { get; set; }

        [Display(Name = "Fighter #1")]
        public Fighter? Fighter1 { get; set; }

        // Note: Fighter2Id, Fighter2, WinnerId, and Winner have to be nullable types.
        // It's a workaround so that SQL is able to delete entries properly.
        // It shouldn't effect anything that we're doing,
        // since we only add entries, not delete them.
        public int? Fighter2Id { get; set; }

        [Display(Name = "Fighter #2")]
        public Fighter? Fighter2 { get; set; }
        public int? WinnerId { get; set; }
        [Display(Name = "Victor")]
        public Fighter? Winner { get; set; }

        //Should format the date like "2024/06/01 @ 16:45:24"
        [DisplayFormat(DataFormatString = "{yyyy/MM/dd @ HH:mm:ss}")]
        public DateTime FightDate { get; set; }

        [Display(Name = "Was this a Daily Feature Match?")]
        public bool DailyFight { get; set; }
    }
}

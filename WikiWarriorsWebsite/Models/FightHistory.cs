
using System.ComponentModel.DataAnnotations;

namespace WikiWarriorsWebsite.Models
{
    public class FightHistory
    {
        public int FightHistoryId { get; set; }
        public int Fighter1Id { get; set; }
        public Fighter? Fighter1 { get; set; }

        // Note: Fighter2Id, Fighter2, WinnerId, and Winner have to be nullable types.
        // It's a workaround so that SQL is able to delete entries properly.
        // It shouldn't effect anything that we're doing,
        // since we only add entries, not delete them.
        public int? Fighter2Id { get; set; }
        public Fighter? Fighter2 { get; set; }
        public int? WinnerId { get; set; }
        public Fighter? Winner { get; set; }
        public DateTime FightDate { get; set; }
        public bool DailyFight { get; set; }
    }
}

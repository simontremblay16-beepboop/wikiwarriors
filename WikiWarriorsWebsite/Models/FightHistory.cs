namespace WikiWarriorsWebsite.Models
{
    public class FightHistory
    {
        public int FightId { get; set; }
        public List<Fighter> Fighters { get; set; }
        public int Fighter1Id { get; set; }
        public int Fighter2Id { get; set; }
        public int WinnerId { get; set; }
        public DateTime FightDate { get; set; }
        public bool DailyFight { get; set; }
    }
}

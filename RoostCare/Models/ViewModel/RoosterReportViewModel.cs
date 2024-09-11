using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel
{
    public class RoosterReportViewModel
    {
        public Rooster Rooster { get; set; }
        public Breed Breed { get; set; }
        public FightHistory FightHistory { get; set; }
        public int CountWin { get; set; }
        public int CountDraw { get; set; }
        public int CountLoss { get; set; }
    }
}

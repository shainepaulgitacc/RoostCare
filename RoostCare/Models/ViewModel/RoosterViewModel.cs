using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel
{
    public class RoosterViewModel
    {
        public Rooster Rooster { get; set; }
        public Breed Breed { get; set; }
        public FightHistory FightHistory { get; set; }
        public Income Income { get; set; }
        public bool HasLoose { get; set; }
    }
}

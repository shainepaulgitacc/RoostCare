using System.ComponentModel.DataAnnotations.Schema;

namespace RoostCare.Models.Domain
{
    public class Rooster : BaseEntity
    {
        
        [ForeignKey("Breed")]
        public int BreedId { get; set; }
        public RoosterCategory RoosterCategory { get; set; }
        public int Price { get; set; }
        public string? RoosterImageFileName { get; set; }

        public RoosterStatus RoosterStatus { get; set; }

        //relational table
        public virtual Breed Breed { get; set; }
        public virtual ICollection<FightHistory> FightHistories { get; set; }
        public virtual ICollection<MedicationHistory> MedicationHistories { get; set; }
       
    }
}

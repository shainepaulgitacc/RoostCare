using System.ComponentModel.DataAnnotations.Schema;

namespace RoostCare.Models.Domain
{
    public class FightHistory:BaseEntity
    {
        [ForeignKey("Rooster")]
        public int RoosterId { get; set; }
        public FightResult FightResult { get; set; }
        public RoosterInjury RoosterInjury { get; set; }
        public DateTime DateOfFight { get; set; }
        public virtual Rooster Rooster { get; set; }
    }
}

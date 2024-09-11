using System.ComponentModel.DataAnnotations.Schema;

namespace RoostCare.Models.Domain
{
    public class MedicationHistory:BaseEntity
    {
        [ForeignKey("Rooster")]
        public int RoosterId { get; set; }
        public DateTime DateAdministered { get; set; }

        public string Vitamin { get; set; }
        public virtual Rooster Rooster { get; set; }
    }
}

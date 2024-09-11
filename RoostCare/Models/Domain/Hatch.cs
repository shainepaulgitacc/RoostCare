using System.ComponentModel.DataAnnotations.Schema;

namespace RoostCare.Models.Domain
{
    public class Hatch:BaseEntity
    {
        [ForeignKey("Incubation")]
        public int IncubationId { get; set; }
        public DateTime HatchingStart { get;  set; }
        public DateTime HatchingEnd { get;  set; }
        public int? NumberOfHatchlings { get; set; } 
        public virtual Incubation Incubation { get; set; }
    }
}

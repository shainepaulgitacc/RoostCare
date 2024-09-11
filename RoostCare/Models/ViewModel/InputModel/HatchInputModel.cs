using System.ComponentModel;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class HatchInputModel:BaseInputModel
    {
        [DisplayName("Incubation BatchId")]
        public int IncubationId { get; set; }
        [DisplayName("Hatching Start")]
        public DateTime HatchingStart { get; set; } = DateTime.Now;
        [DisplayName("Hatching End")]
        public DateTime HatchingEnd { get; set; } = DateTime.Now;
        [DisplayName("Number Of Hatchlings")]
        public int? NumberOfHatchlings { get; set; }
    }
}

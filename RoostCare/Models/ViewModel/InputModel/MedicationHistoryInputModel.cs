using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class MedicationHistoryInputModel:BaseInputModel
    {
        public int RoosterId { get; set; }
        [Required,DisplayName("Date Administered")]
        public DateTime DateAdministered { get; set; }
        [Required,DisplayName("Rooster Vitamin")]
        public string Vitamin { get; set; }
    }
}

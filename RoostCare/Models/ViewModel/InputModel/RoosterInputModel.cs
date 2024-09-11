using RoostCare.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class RoosterInputModel:BaseInputModel
    {
        [Required,DisplayName("Breed")]
        public int BreedId { get; set; }

        [Required, DisplayName("Rooster Category")]
        public RoosterCategory RoosterCategory { get; set; }
        public int? Price { get; set; }
        [DisplayName("Rooster Profile")]
        public IFormFile? RImageFile { get; set; }

        [Required,DisplayName("Rooster Status")]
        public RoosterStatus RoosterStatus { get; set; }
    }
}

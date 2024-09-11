using RoostCare.Models.Infrastracture.Implementation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class BreedInputModel : BaseInputModel
    {
        [DisplayName("Breed Name"), Required]
        public string BreedName { get; set; }
    }
}

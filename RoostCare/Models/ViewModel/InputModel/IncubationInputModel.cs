using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class IncubationInputModel:BaseInputModel
    {
        [Required, DisplayName("Date Laid")]
        public DateTime DateLaid { get; set; } = DateTime.Now;
        [Required, DisplayName("Number Of Eggs")]
        public int NumberOfEggs { get; set; }
        [Required, DisplayName("Incubation Start")]
        public DateTime IncubationStart { get; set; } = DateTime.Now;
        [Required, DisplayName("Incubation Complete")]
        public DateTime IncubationComplete { get; set; } = DateTime.Now;

        [DisplayName("Number of Non Viable Eggs")]
        public int? NumberOfNonViableEggs { get; set; }
        [DisplayName("Number of Viable Eggs")]
        public int? NumberOfViableEggs { get; set; }
    }
}

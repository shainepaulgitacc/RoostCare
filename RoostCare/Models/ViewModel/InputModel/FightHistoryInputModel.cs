using RoostCare.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class FightHistoryInputModel:BaseInputModel
    {
        public int RoosterId { get; set; }
        [Required,DisplayName("Fight Result")]
        public FightResult FightResult { get; set; }
        [Required,DisplayName("Rooster's Injury")]
        public RoosterInjury RoosterInjury { get; set; }
        [Required,DisplayName("Date of the fight")]
        public DateTime DateOfFight { get; set; }
    }
}

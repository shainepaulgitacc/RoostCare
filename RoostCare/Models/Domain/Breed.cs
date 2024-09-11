namespace RoostCare.Models.Domain
{
    public class Breed : BaseEntity
    {
        public string BreedName { get; set; }

        public virtual ICollection<Rooster> Roosters { get; set; }
    }
}

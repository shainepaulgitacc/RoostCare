using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoostCare.Models.Domain;

namespace RoostCare.Models.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Rooster> Roosters { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<FightHistory> FightHistories { get; set; }
        public virtual DbSet<MedicationHistory> MedicationHistories { get; set;}
        public virtual DbSet<Hatch> HatchingTrackers { get; set; }
        public virtual DbSet<Incubation>IncubationTrackers { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Income> Incomes { get;  set; }
        public virtual DbSet<NotificationEntity> Notifications { get; set; }
    }
}

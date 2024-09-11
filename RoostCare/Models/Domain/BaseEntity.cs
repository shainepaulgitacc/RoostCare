namespace RoostCare.Models.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
    public enum RoosterCategory
    {
        ForSale,
        ForCockFight
    }
    public enum FightResult
    {
        Win,Draw,Loss
    }
    public enum RoosterInjury
    {
        Minor,Major,None
    }
    public enum ExpensesCategories
    {
        Utilities, Medications, FeedSupply,Other
    }
    public enum ProductType
    {
        Chicks,Roosters
    }
    public enum IncomeCategories
    {
        CockFightEarnings,
        LiveStockSalesChicks,
        LiveStockSalesRoosters
    }
    public enum RoosterStatus
    {
        ReadyToFight,
        Healthy,
        UnderMaintenance,
        Injured
    }
    public enum Sex
    {
        Male,
        Female
    }
    public enum NotifActionType
    {
        View,
        Delete,
        Archived,
        Restore
    }
}

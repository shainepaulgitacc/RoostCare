using RoostCare.Models.Domain;
using RoostCare.Models.ViewModel;

namespace RoostCare.Models.Infrastracture
{
    public interface IRoosterRepository:IBaseRepository<Rooster>
    {
        Task<IEnumerable<RoosterViewModel>> RoosterRecords();
        Task<IEnumerable<RoosterReportViewModel>> RoosterReportRecords(DateTime? from, DateTime? to);
    }
}

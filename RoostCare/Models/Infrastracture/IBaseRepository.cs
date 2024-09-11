namespace RoostCare.Models.Infrastracture
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(string Id);
        Task Add(object obj);
        Task Update(string Id ,object obj);
        Task Delete(string Id);
    }
}

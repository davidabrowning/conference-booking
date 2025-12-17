namespace ConferenceBooking.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}

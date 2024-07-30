namespace SingleRepoPokemonApi.Repositories.IRepo;

public interface IRepo<T> where T : class
{
    Task<int> AddAsync(T entity);


    Task<int> UpdateAsync(T entity);


    Task<int> DeleteAsync(T entity);


    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();
}

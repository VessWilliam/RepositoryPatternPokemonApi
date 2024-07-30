using Microsoft.EntityFrameworkCore;
using SingleRepoPokemonApi.Data;
using SingleRepoPokemonApi.Repositories.IRepo;

namespace SingleRepoPokemonApi.Repositories;

public class Repo<T> : IRepo<T> where T : class
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    public Repo(IDbContextFactory<AppDbContext> contextFactory) => _contextFactory = contextFactory;

    public async Task<int> AddAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        await context.Set<T>().AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().FindAsync(id) ?? null!;

    }

    public async Task<int> UpdateAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync();
    }
}

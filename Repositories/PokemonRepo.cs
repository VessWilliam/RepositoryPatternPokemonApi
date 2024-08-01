using Microsoft.EntityFrameworkCore;
using SingleRepoPokemonApi.Data;
using SingleRepoPokemonApi.Model.Entity;
using SingleRepoPokemonApi.Repositories.IRepo;

namespace SingleRepoPokemonApi.Repositories;

public class PokemonRepo : Repo<Pokemon>, IPokemonRepo
{
    private readonly IDbContextFactory<AppDbContext> _appDbContext;

    public PokemonRepo(IDbContextFactory<AppDbContext> appDbContext) : base(appDbContext) => _appDbContext = appDbContext;

    public Task<Pokemon?> AddPokemonAsync(Pokemon pokemon)
    {
        throw new NotImplementedException();
    }

    public async Task<Pokemon?> GetPokemonByIdAsync(string id)
    {
        using var context = _appDbContext.CreateDbContext();
        return await context.Pokemons
            .Include(e => e.PokemonAttributes)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task<List<Pokemon>> GetAllPokemonAsync()
    {
        using var context = _appDbContext.CreateDbContext();
        return await context.Pokemons
            .Include(e => e.PokemonAttributes)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> UpdatePokemonAsync(string id, Pokemon pokemon)
    {
        try
        {
            using var context = _appDbContext.CreateDbContext();

            var currententity = await context.Pokemons
                 .Include(x => x.PokemonAttributes)
                 .FirstOrDefaultAsync(x => x.Id!.Equals(id));

            if (currententity is null) return false;

            if (currententity!.PokemonAttributes is not null)
            {
                foreach (var item in currententity.PokemonAttributes)
                {
                    context.PokemonAttributes.Remove(item);
                }
            }

            currententity.Name = pokemon.Name;

            if (pokemon.PokemonAttributes?.Count > 0)
            {
                foreach (var item in pokemon.PokemonAttributes)
                {
                    currententity.PokemonAttributes?.Add(item);
                }
            }

            context.Update(currententity);
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

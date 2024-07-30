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
            .Include(e => e.pokemonAttribute)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task<List<Pokemon>> GetAllPokemonAsync()
    {
        using var context = _appDbContext.CreateDbContext();
        return await context.Pokemons
            .Include(e => e.pokemonAttribute)
            .AsNoTracking()
            .ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using SingleRepoPokemonApi.Model.Entity;

namespace SingleRepoPokemonApi.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonAttribute> PokemonAttributes { get; set; }
}

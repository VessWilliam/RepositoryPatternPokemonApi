using SingleRepoPokemonApi.Model.Entity;

namespace SingleRepoPokemonApi.Repositories.IRepo;

public interface IPokemonRepo : IRepo<Pokemon>
{
    Task<List<Pokemon>> GetAllPokemonAsync();
    Task<Pokemon?> GetPokemonByIdAsync(string id);
    Task<bool> UpdatePokemonAsync(string id, Pokemon pokemon);
}

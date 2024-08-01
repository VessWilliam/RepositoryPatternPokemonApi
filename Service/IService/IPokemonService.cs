using SingleRepoPokemonApi.Model.DTO;

namespace SingleRepoPokemonApi.Service.IService;

public interface IPokemonService
{
    Task<PokemonDTO?> AddPokemonAsync(PokemonDTO pokemon);
    Task<bool> DeletePokemonAsync(string id);
    Task<List<PokemonDTO>?> GetAllPokemonAsync();
    Task<PokemonDTO?> UpdatePokemonAsync(string id, PokemonDTO pokemon);
}

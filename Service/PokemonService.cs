using SingleRepoPokemonApi.Model.DTO;
using SingleRepoPokemonApi.Model.Entity;
using SingleRepoPokemonApi.Repositories.IRepo;
using SingleRepoPokemonApi.Service.IService;

namespace SingleRepoPokemonApi.Service;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepo _pokemonRepo;

    public PokemonService(IPokemonRepo pokemonRepo)
    {
        _pokemonRepo = pokemonRepo;
    }

    public async Task<List<PokemonDTO>?> GetAllPokemonAsync()
    {
        try
        {
            var entityList = await _pokemonRepo.GetAllPokemonAsync();

            if (!entityList.Any()) return new();

            var dtolist = entityList.Select(e => new PokemonDTO
            {
                Id = e.Id,
                Name = e.Name,
                Attribute = e.pokemonAttribute?.Select(e => new PokemonAttributeDTO
                {
                    Attack = e.Attack,
                    Def = e.Def,
                    Type = e.Type,

                }).ToList(),
            }).ToList();

            return dtolist;
        }
        catch (Exception)
        {
            return new();
        }
    }

    public async Task<PokemonDTO?> AddPokemonAsync(PokemonDTO pokemon)
    {
        try
        {
            if (string.IsNullOrEmpty(pokemon.Name)) return null;

            var newpokemon = new Pokemon
            {
                Name = pokemon.Name,
                pokemonAttribute = pokemon.Attribute?
                .Where(e => !string.IsNullOrEmpty(e.Type))
                .Select(e => new PokemonAttribute
                {
                    Def = e.Def,
                    Attack = e.Attack,
                    Type = e.Type,
                }).ToList()
            };

            if (await _pokemonRepo.AddAsync(newpokemon) <= 0) return null;

            pokemon.Id = newpokemon.Id;

            return pokemon;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PokemonDTO?> UpdatePokemonAsync(PokemonDTO pokemon)
    {
        try
        {
            if (string.IsNullOrEmpty(pokemon.Name) ||
                string.IsNullOrEmpty(pokemon.Id)) return null;

            var currentpokemon = await _pokemonRepo.GetPokemonByIdAsync(pokemon.Id);

            if (currentpokemon is null) return null;

            currentpokemon.Name = pokemon.Name;

            if (pokemon.Attribute?.Any() == true)
            {
                foreach (var item in pokemon.Attribute)
                {
                    if (currentpokemon.pokemonAttribute!.Any(e => e.Id.Equals(item.id))) continue;

                    currentpokemon.pokemonAttribute?.Add(new PokemonAttribute
                    {
                        Attack = item.Attack,
                        Def = item.Def,
                        Type = item.Type,
                        PokemonId = pokemon.Id,
                    });
                }
            }

            if (await _pokemonRepo.UpdateAsync(currentpokemon) <= 0) return null;

            var endresult = new PokemonDTO
            {
                Id = currentpokemon.Id,
                Name = currentpokemon.Name,
                Attribute = currentpokemon.pokemonAttribute?.Select(e => new PokemonAttributeDTO
                {
                    Attack = e.Attack,
                    Def = e.Def,
                    Type = e.Type,

                }).ToList(),
            };

            return endresult;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> DeletePokemonAsync(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) return false;

            var currentpokemon = await _pokemonRepo.GetPokemonByIdAsync(id);

            if (currentpokemon is null) return false;

            return await _pokemonRepo.DeleteAsync(currentpokemon) > 0 ? true : false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

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
                Attribute = e.PokemonAttributes?.Select(e => new PokemonAttributeDTO
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
                PokemonAttributes = pokemon.Attribute?
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

    public async Task<PokemonDTO?> UpdatePokemonAsync(string id, PokemonDTO pokemon)
    {
        try
        {
            var updatePokemon = new Pokemon
            {
                Id = id,
                Name = pokemon.Name,
                PokemonAttributes = pokemon.Attribute?
                .Where(e => !string.IsNullOrEmpty(e.Type))
                .Select(e => new PokemonAttribute
                {
                    Attack = e.Attack,
                    Def = e.Def,
                    Type = e.Type
                }).ToList()
            };

            var updateResult = await _pokemonRepo.UpdatePokemonAsync(id, updatePokemon);

            if (!updateResult) return null;

            var endResult = new PokemonDTO
            {
                Id = updatePokemon.Id,
                Name = updatePokemon.Name,
                Attribute = updatePokemon.PokemonAttributes?
                    .Select(e => new PokemonAttributeDTO
                    {
                        Attack = e.Attack,
                        Def = e.Def,
                        Type = e.Type,
                    }).ToList(),
            };

            return endResult;
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

            var currentPokemon = await _pokemonRepo.GetPokemonByIdAsync(id);

            if (currentPokemon == null ||
                (currentPokemon.PokemonAttributes != null
                && currentPokemon.PokemonAttributes.Count > 0)) return false;

            return await _pokemonRepo.DeleteAsync(currentPokemon) > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

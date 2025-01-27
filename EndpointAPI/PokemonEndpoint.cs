﻿using Microsoft.AspNetCore.Mvc;
using SingleRepoPokemonApi.Model.DTO;
using SingleRepoPokemonApi.Service.IService;

namespace SingleRepoPokemonApi.EndpointAPI;

public static class PokemonEndpoint
{
    public static void PokemonApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pokemonGet", async (IPokemonService pokemonService) =>
        {
            var resultlist = await pokemonService.GetAllPokemonAsync();

            return resultlist?.Count <= 0 ? Results.BadRequest(new { message = "Pokemon get unsuccessful." }) : Results.Ok(resultlist);
        });

        app.MapPost("/pokemonCreate", async (IPokemonService pokemonService, [FromBody] PokemonDTO pokemon) =>
        {
            var result = await pokemonService.AddPokemonAsync(pokemon);

            return result is null ? Results.BadRequest(new { message = "Pokemon create unsuccessful." }) : Results.Ok(result);
        });

        app.MapPut("/pokemonUpdate", async (IPokemonService pokemonService, string id, [FromBody] PokemonDTO pokemon) =>
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pokemon?.Name))
            {
                return Results.BadRequest(new { message = "Pokemon update unsuccessful." });
            }

            var result = await pokemonService.UpdatePokemonAsync(id, pokemon);

            return result is null ? Results.BadRequest(new { message = "Pokemon update unsuccessful." }) : Results.Ok(result);
        });

        app.MapDelete("/pokemonDelete", async (IPokemonService pokemonservice, string id) =>
        {
            if (string.IsNullOrEmpty(id))
            {
                return Results.BadRequest(new { message = "Pokemon delete unsuccessful." });
            }

            var result = await pokemonservice.DeletePokemonAsync(id);

            return result is false ? Results.BadRequest(new { message = "Pokemon delete unsuccessful." })
                                     : Results.Ok(new { message = "Pokemon successfully deleted." }); ;

        });
    }
}

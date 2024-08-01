using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SingleRepoPokemonApi.Model.DTO;

public class PokemonAttributeDTO
{
    [DefaultValue("")]
    public int Attack { get; set; }

    [DefaultValue("")]
    public int Def { get; set; }

    [DefaultValue("")]
    public string? Type { get; set; }
}

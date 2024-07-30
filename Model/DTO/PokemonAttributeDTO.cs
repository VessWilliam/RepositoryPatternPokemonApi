using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SingleRepoPokemonApi.Model.DTO;

public class PokemonAttributeDTO
{
    [JsonIgnore]
    public int id { get; set; }

    [DefaultValue("")]
    public int Attack { get; set; }

    [DefaultValue("")]
    public int Def { get; set; }

    [DefaultValue("")]
    public string? Type { get; set; }

    [JsonIgnore]
    public string? PokemonId { get; set; }
}

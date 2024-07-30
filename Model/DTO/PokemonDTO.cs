using System.ComponentModel;
namespace SingleRepoPokemonApi.Model.DTO;

public class PokemonDTO
{
    [DefaultValue("")]
    public string? Id { get; set; }

    [DefaultValue("")]
    public string? Name { get; set; }
    public List<PokemonAttributeDTO>? Attribute { get; set; }
}

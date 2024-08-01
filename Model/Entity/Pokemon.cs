using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SingleRepoPokemonApi.Model.Entity;

[Table(nameof(Pokemon))]
public class Pokemon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public ICollection<PokemonAttribute>? PokemonAttributes { get; set; }
}

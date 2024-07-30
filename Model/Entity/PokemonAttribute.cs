using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleRepoPokemonApi.Model.Entity;

[Table(nameof(PokemonAttribute))]
public class PokemonAttribute
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Attack { get; set; }
    public int Def { get; set; }
    public string? Type { get; set; }

    [ForeignKey(nameof(Pokemon))]
    public string? PokemonId { get; set; }
    public Pokemon? pokemon { get; set; }
}

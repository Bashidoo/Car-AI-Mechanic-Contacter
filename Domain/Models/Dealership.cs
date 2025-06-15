using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Dealership
{
    [Key]
    public int DealershipId { get; set; }

    [Required, MaxLength(100)]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [Required, MaxLength(150)]
    [Column(TypeName = "nvarchar(150)")]
    public string Email { get; set; }

    public ICollection<DealershipResponse> Responses { get; set; }
}

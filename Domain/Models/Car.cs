using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Car
{
    [Key]
    public int CarId { get; set; }

    [Required, MaxLength(100)]
    [Column(TypeName = "nvarchar(100)")]
    public string Model { get; set; }

    [Required, MaxLength(1000)]
    [Column(TypeName = "nvarchar(1000)")]
    public string Description { get; set; }

    [MaxLength(255)]
    [Column(TypeName = "nvarchar(255)")]
    public string ImagePath { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string AIAnalysis { get; set; }

    public ICollection<DealershipResponse> Responses { get; set; }
}

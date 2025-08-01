using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("CarIssue")]
    public class CarIssue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Explicit
        public int CarIssueId { get; set; }

        [Required, MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? OptionalComment { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string AIAnalysis { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }
    }
}
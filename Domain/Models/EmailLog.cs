using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarDealership.Domain.Models;

public class EmailLog
{
    [Key]
    public int EmailLogId { get; set; }

    [Required]
    public int CarId { get; set; }

    [ForeignKey(nameof(CarId))]
    public Car Car { get; set; }

    [Required]
    public int DealershipId { get; set; }

    [ForeignKey(nameof(DealershipId))]
    public Dealership Dealership { get; set; }

    [Required, MaxLength(150)]
    [Column(TypeName = "nvarchar(150)")]
    public string ToEmail { get; set; }

    [Required, MaxLength(200)]
    [Column(TypeName = "nvarchar(200)")]
    public string Subject { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string Body { get; set; }

    [MaxLength(255)]
    [Column(TypeName = "nvarchar(255)")]
    public string MessageId { get; set; }

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

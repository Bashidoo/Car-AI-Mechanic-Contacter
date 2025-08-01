using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

public class DealershipResponse
{
    [Key]
    public int ResponseId { get; set; }

    [Required]
    public int CarId { get; set; }

    [ForeignKey(nameof(CarId))]
    public Car Car { get; set; }

    [Required]
    public int DealershipId { get; set; }

    [ForeignKey(nameof(DealershipId))]
    public Dealership Dealership { get; set; }

    [Required, MaxLength(200)]
    [Column(TypeName = "nvarchar(200)")]
    public string EmailSubject { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string EmailBody { get; set; }

    [Required, MaxLength(150)]
    [Column(TypeName = "nvarchar(150)")]
    public string FromEmail { get; set; }

    [MaxLength(255)]
    [Column(TypeName = "nvarchar(255)")]
    public string MessageId { get; set; }

    [MaxLength(255)]
    [Column(TypeName = "nvarchar(255)")]
    public string InReplyToMessageId { get; set; }

    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required, MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Model { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string? RegistrationNumber { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string? ImagePath { get; set; }

        // Navigation property to related car issues
        public ICollection<CarIssue> Issues { get; set; }

        // Navigation property to related dealership responses (om du har det)
        //public ICollection<DealershipResponse> Responses { get; set; }
    }
}


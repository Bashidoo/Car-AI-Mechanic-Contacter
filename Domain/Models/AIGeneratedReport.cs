using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AIGeneratedReport
    {
        [Key]
        public int AIDescriptionId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string DescriptionText { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

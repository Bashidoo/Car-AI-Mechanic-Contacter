using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.AIGeneratedReportDtos
{
    public class AIGeneratedReportCreateDto
    {
        public int CarId { get; set; }

        [MaxLength(1000)]
        public string DescriptionText { get; set; }
    }

}

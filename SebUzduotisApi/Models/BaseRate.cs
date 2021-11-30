using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SebUzduotisApi.Models
{
    public class BaseRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BaseRateId { get; set; }

        [Required]
        public string BaseRateCode { get; set; }

        public List<Agreement> Agreements { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SebUzduotisApi.Models
{
    public class Agreement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgreementId { get; set; }

        public decimal Amount { get; set; }
        public decimal Margin { get; set; }
        public int Duration { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int BaseRateId { get; set; }
        public BaseRate BaseRate { get; set; }
    }
}

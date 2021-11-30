using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SebUzduotisApi.Views
{
    public class InterestRatesView
    {
        public decimal LastInterestRate { get; set; }
        public decimal NewInterestRate { get; set; }
        public decimal Difference { get; set; }
    }
}

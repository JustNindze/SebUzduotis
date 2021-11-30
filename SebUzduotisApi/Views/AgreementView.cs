using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SebUzduotisApi.Views
{
    public class AgreementView
    {
        public decimal Amount { get; set; }
        public string BaseRateCode { get; set; }
        public decimal Margin { get; set; }
        public int Duration { get; set; }
    }
}

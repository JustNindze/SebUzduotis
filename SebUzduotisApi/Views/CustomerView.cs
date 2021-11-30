using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SebUzduotisApi.Views
{
    public class CustomerView
    {
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AgreementView LastAgreement { get; set; }
        public InterestRatesView InterestRates { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SebUzduotisApi.Context;
using SebUzduotisApi.Models;
using SebUzduotisApi.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace SebUzduotisApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("AddAgreementToUser")]
        public object AddAgreementToUser(string personalId, AgreementView newAgreement)
        {
            if (personalId == string.Empty || personalId == null)
            {
                return BadRequest("PersonalId has empty value");
            }

            if (newAgreement == null)
            {
                return BadRequest("Bad request");
            }

            var customerView = new CustomerView();
            customerView.LastAgreement = new AgreementView();
            customerView.InterestRates = new InterestRatesView();

            using (var context = new DataContext())
            {
                var customer = context.Customers.FirstOrDefault(x => x.PersonalId == personalId);
                if (customer == null)
                {
                    return NotFound(string.Format("Customer with PersonalId '{0}' is not found", personalId));
                }

                customerView.PersonalId = customer.PersonalId;
                customerView.FirstName = customer.FirstName;
                customerView.LastName = customer.LastName;

                var lastAgreement = context.Agreements.OrderByDescending(x => x.AgreementId).FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (lastAgreement == null)
                {
                    return NotFound("Customer has no agreements");
                }

                var lastBaseRate = context.BaseRates.FirstOrDefault(x => x.BaseRateId == lastAgreement.BaseRateId);
                var lastBaseRateVilibRate = GetLatestVilibRate(lastBaseRate.BaseRateCode);
                if (lastBaseRateVilibRate == null)
                {
                    return Problem("Can't get LatestVilibRate value from server", null, null, "No LatestVilibRate value");
                }

                customerView.LastAgreement.Amount = lastAgreement.Amount;
                customerView.LastAgreement.BaseRateCode = lastBaseRate.BaseRateCode;
                customerView.LastAgreement.Margin = lastAgreement.Margin;
                customerView.LastAgreement.Duration = lastAgreement.Duration;

                var newBaseRate = context.BaseRates.FirstOrDefault(x => x.BaseRateCode == newAgreement.BaseRateCode);
                if (newBaseRate == null)
                {
                    return NotFound(string.Format("BaseRateCode '{0}' is not found", newAgreement.BaseRateCode));
                }
                var newBaseRateVilibRate = GetLatestVilibRate(newBaseRate.BaseRateCode);
                if (lastBaseRateVilibRate == null)
                {
                    return Problem("Can't get LatestVilibRate value from server", null, null, "No LatestVilibRate value");
                }

                try
                {
                    context.Agreements.Add(new Agreement
                    {
                        Amount = newAgreement.Amount,
                        Margin = newAgreement.Margin,
                        Duration = newAgreement.Duration,
                        CustomerId = customer.CustomerId,
                        BaseRateId = newBaseRate.BaseRateId
                    });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message, null, null, "Exception occured while writing to Database");
                }

                var lastInterestRate = (decimal)lastBaseRateVilibRate + lastAgreement.Margin;
                var newInterestRate = (decimal)newBaseRateVilibRate + newAgreement.Margin;
                var difference = lastInterestRate - newInterestRate;
                customerView.InterestRates.LastInterestRate = lastInterestRate;
                customerView.InterestRates.NewInterestRate = newInterestRate;
                customerView.InterestRates.Difference = difference;
            }

            return Ok(customerView);
        }

        [NonAction]
        public decimal? GetLatestVilibRate(string baseRateCode)
        {
            var uri = "http://www.lb.lt/webservices/VilibidVilibor/VilibidVilibor.asmx/getLatestVilibRate?RateType=";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + baseRateCode);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();

                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(result);
                    var vilibRate = XmlConvert.ToDecimal(xmlDocument.InnerText);

                    return vilibRate;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

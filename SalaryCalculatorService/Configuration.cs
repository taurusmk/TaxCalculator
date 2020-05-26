using System;
using System.Collections.Generic;
using System.Text;
using ISalaryCalculatorServices;
using System.Configuration;

namespace SalaryCalculatorServices
{
    public class Configuration : IConfiguration
    {
        public decimal IncomeTaxValue => Decimal.Parse(ConfigurationManager.AppSettings["IncomeTaxValue"]);
        public decimal IncomeTaxBoundary => Decimal.Parse(ConfigurationManager.AppSettings["IncomeTaxBoundary"]);
        public decimal SocialContributionTaxValue => Decimal.Parse(ConfigurationManager.AppSettings["SocialContributionTaxValue"]);
        public decimal SocialContributionTaxLowerBoundary => Decimal.Parse(ConfigurationManager.AppSettings["SocialContributionTaxLowerBoundary"]);
        public decimal SocialContributionTaxUpperBoundary => Decimal.Parse(ConfigurationManager.AppSettings["SocialContributionTaxUpperBoundary"]);
    }
}

using System;
using ISalaryCalculatorServices;
using System.Configuration;

namespace SalaryCalculatorServices
{
    public class SalaryCalculatorService : ISalaryCalculatorService
    {
        private readonly decimal incomeTaxValue;
        private readonly decimal incomeTaxBoundary;

        private readonly decimal socialContributionTaxValue;
        private readonly decimal socialContributionTaxLowerBoundary;
        private readonly decimal socialContributionTaxUpperBoundary;

        public SalaryCalculatorService()
        {
            incomeTaxValue = Decimal.Parse(ConfigurationManager.AppSettings["IncomeTaxValue"]);
            incomeTaxBoundary = Decimal.Parse(ConfigurationManager.AppSettings["IncomeTaxBoundary"]);

            socialContributionTaxValue = Decimal.Parse(ConfigurationManager.AppSettings["SocialContributionTaxValue"]);
            socialContributionTaxLowerBoundary = Decimal.Parse(ConfigurationManager.AppSettings["SocialContributionTaxLowerBoundary"]);
            socialContributionTaxUpperBoundary = Decimal.Parse(ConfigurationManager.AppSettings["SocialContributionTaxUpperBoundary"]);
        }

        public decimal CalculateNetSalary(decimal grossSalary)
        {
            decimal incomeTaxAmount = CalculateIncomeTax(grossSalary);
            decimal socialContributionTaxAmount = CalculateSocialContributionTax(grossSalary);
            return grossSalary - incomeTaxAmount - socialContributionTaxAmount;
        }

        public decimal CalculateIncomeTax(decimal grossSalary)
        {
            if (grossSalary > incomeTaxBoundary)
            {
                return (grossSalary - incomeTaxBoundary) * incomeTaxValue;
            }

            return 0;
        }

        public decimal CalculateSocialContributionTax(decimal grossSalary)
        {
            if (grossSalary > socialContributionTaxLowerBoundary)
            {
                decimal amountForTaxation =
                    (grossSalary - socialContributionTaxLowerBoundary) > (socialContributionTaxUpperBoundary - socialContributionTaxLowerBoundary)
                        ? (socialContributionTaxUpperBoundary - socialContributionTaxLowerBoundary)
                        : (grossSalary - socialContributionTaxLowerBoundary);
                return amountForTaxation * socialContributionTaxValue;
            }
            else
            {
                return 0;
            }
        }
    }
}

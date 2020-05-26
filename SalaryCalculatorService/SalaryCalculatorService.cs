using System;
using ISalaryCalculatorServices;

namespace SalaryCalculatorServices
{
    public class SalaryCalculatorService : ISalaryCalculatorService
    {
        private IConfiguration configuration;
        public SalaryCalculatorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal CalculateNetSalary(decimal grossSalary)
        {
            decimal incomeTaxAmount = CalculateIncomeTax(grossSalary);
            decimal socialContributionTaxAmount = CalculateSocialContributionTax(grossSalary);
            return grossSalary - incomeTaxAmount - socialContributionTaxAmount;
        }

        public decimal CalculateIncomeTax(decimal grossSalary)
        {
            if (grossSalary > configuration.IncomeTaxBoundary)
            {
                return (grossSalary - configuration.IncomeTaxBoundary) * configuration.IncomeTaxValue;
            }

            return 0;
        }

        public decimal CalculateSocialContributionTax(decimal grossSalary)
        {
            if (grossSalary > configuration.SocialContributionTaxLowerBoundary)
            {
                decimal amountForTaxation =
                    (grossSalary - configuration.SocialContributionTaxLowerBoundary) > (configuration.SocialContributionTaxUpperBoundary - configuration.SocialContributionTaxLowerBoundary)
                        ? (configuration.SocialContributionTaxUpperBoundary - configuration.SocialContributionTaxLowerBoundary)
                        : (grossSalary - configuration.SocialContributionTaxLowerBoundary);
                return amountForTaxation * configuration.SocialContributionTaxValue;
            }
            else
            {
                return 0;
            }
        }
    }
}

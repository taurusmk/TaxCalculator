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
                decimal basisForTaxation = grossSalary - configuration.IncomeTaxBoundary;
                return basisForTaxation * configuration.IncomeTaxValue;
            }

            return 0;
        }

        public decimal CalculateSocialContributionTax(decimal grossSalary)
        {
            if (grossSalary > configuration.SocialContributionTaxLowerBoundary)
            {
                decimal basisForTaxation = grossSalary - configuration.SocialContributionTaxLowerBoundary;
                decimal maxAmountForTaxation = configuration.SocialContributionTaxUpperBoundary - configuration.SocialContributionTaxLowerBoundary;
                decimal amountForTaxation = basisForTaxation > maxAmountForTaxation ? maxAmountForTaxation : basisForTaxation;
                return amountForTaxation * configuration.SocialContributionTaxValue;
            }

            return 0;
        }
    }
}

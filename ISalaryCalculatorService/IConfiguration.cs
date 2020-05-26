using System;
using System.Collections.Generic;
using System.Text;

namespace ISalaryCalculatorServices
{
    public interface IConfiguration
    {
        decimal IncomeTaxValue { get; }
        decimal IncomeTaxBoundary { get; }

        decimal SocialContributionTaxValue { get; }
        decimal SocialContributionTaxLowerBoundary { get; }
        decimal SocialContributionTaxUpperBoundary { get; }
    }
}

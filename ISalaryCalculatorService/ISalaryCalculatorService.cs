namespace ISalaryCalculatorServices
{
    public interface ISalaryCalculatorService
    {
        decimal CalculateNetSalary(decimal grossSalary);
        decimal CalculateIncomeTax(decimal grossSalary);
        decimal CalculateSocialContributionTax(decimal grossSalary);
    }
}
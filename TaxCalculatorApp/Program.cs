using System;
using System.Configuration;
using ISalaryCalculatorServices;
using Microsoft.Extensions.DependencyInjection;
using SalaryCalculatorServices;

namespace TaxCalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<ISalaryCalculatorService, SalaryCalculatorService>()
                .BuildServiceProvider();

            do
            {
                decimal grossSalary;
                string grossSalaryString = String.Empty;

                Console.Write(StringConstants.EnterGrossSalary);
                grossSalaryString = Console.ReadLine();

                if (!decimal.TryParse(grossSalaryString, out grossSalary))
                {
                    Console.WriteLine(StringConstants.EnterValidNumber);
                    continue;
                }

                ISalaryCalculatorService calculatorService = serviceProvider.GetService<ISalaryCalculatorService>();

                decimal netSalary = calculatorService.CalculateNetSalary(grossSalary);

                Console.WriteLine(StringConstants.NetSalary, netSalary);
            } while (true);
        }
    }
}

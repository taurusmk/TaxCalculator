using ISalaryCalculatorServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SalaryCalculatorServices;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTests
{
    [TestClass]
    public class SalaryCalculatorServiceTests
    {
        private readonly Mock<IConfiguration> mockConfiguration;
        public SalaryCalculatorServiceTests()
        {
            mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x.IncomeTaxValue).Returns(0.1M);
            mockConfiguration.SetupGet(x => x.IncomeTaxBoundary).Returns(1000M);
            mockConfiguration.SetupGet(x => x.SocialContributionTaxValue).Returns(0.15M);
            mockConfiguration.SetupGet(x => x.SocialContributionTaxLowerBoundary).Returns(1000M);
            mockConfiguration.SetupGet(x => x.SocialContributionTaxUpperBoundary).Returns(3000M);
        }

        [TestCase(980, 980)]
        [TestCase(1100, 1075)]
        [TestCase(3000, 2500)]
        [TestCase(3400, 2860)]
        [TestCase(5000, 4300)]
        public void CalculateNetSalaryTest(decimal grossSalary, decimal netSalary)
        {
            //setup
            ISalaryCalculatorService service = new SalaryCalculatorService(mockConfiguration.Object);

            //run
            decimal result = service.CalculateNetSalary(grossSalary);

            //assert
            Assert.AreEqual(netSalary, result);

        }

        [TestCase(1010, 1)]
        [TestCase(990, 0)]
        public void CalculateIncomeTaxTest(decimal grossSalary, decimal taxAmount)
        {
            //setup
            ISalaryCalculatorService service = new SalaryCalculatorService(mockConfiguration.Object);

            //run
            decimal result = service.CalculateIncomeTax(grossSalary);

            //assert
            Assert.AreEqual(taxAmount, result);

        }

        [TestCase(990, 0)]
        [TestCase(1100, 15)]
        [TestCase(3500, 300)]
        public void CalculateSocialContributionTaxTest(decimal grossSalary, decimal taxAmount)
        {
            //setup
            ISalaryCalculatorService service = new SalaryCalculatorService(mockConfiguration.Object);

            //run
            decimal result = service.CalculateSocialContributionTax(grossSalary);

            //assert
            Assert.AreEqual(taxAmount, result);

        }
    }
}

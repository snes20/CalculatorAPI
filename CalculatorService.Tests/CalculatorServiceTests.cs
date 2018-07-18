using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorService.Controllers;
using Newtonsoft.Json.Linq;

namespace CalculatorService.Tests
{
    [TestClass]
    public class CalculatorServiceTests
    {
        CalculatorController  _calculadora;


        [TestInitialize]
        public void TestInicialization()
        {
            _calculadora = new CalculatorController();
        }


        [TestMethod]
        public void TestAddMultipleValues()
        {
            var json = @"{'Addends':[1,2,3]}";

            var result = _calculadora.Addition(JObject.Parse(json));
            var jsonResult = JObject.Parse(result);

            Assert.IsTrue(jsonResult["Sum"] != null);
            Assert.IsTrue(jsonResult["Sum"].ToObject<int>() == 6);
        }

        [TestMethod]
        public void TestAddMultipleValuesWithTrakingId()
        {
            var json = @"{'Addends':[5,2,3], 'Tracking-Id':20}";

            var result = _calculadora.Addition(JObject.Parse(json));

            var jsonResult = JObject.Parse(result);
            Assert.IsTrue(jsonResult["Sum"] != null);
            Assert.IsTrue(jsonResult["Sum"].ToObject<int>() == 10);
        }

        [TestMethod]
        public void TestSubtractValues()
        {
            var json = "{ \"Minuend\": 5, \"Subtrahend\": 1}"; 

            var result = _calculadora.Subtraction(JObject.Parse(json));
            var jsonResult = JObject.Parse(result);

            Assert.IsTrue(jsonResult["Difference"] != null);
            Assert.IsTrue(jsonResult["Difference"].ToObject<int>() == 4);

        }

        [TestMethod]
        public void TestMultiplyValues()
        {
            var json = @"{'Factors' : [8,3,2]}";

            var result = _calculadora.Multiply(JObject.Parse(json));
            var jsonResult = JObject.Parse(result);

            Assert.IsTrue(jsonResult["Product"] != null);
            Assert.IsTrue(jsonResult["Product"].ToObject<int>() == 48);

        }

        [TestMethod]
        public void TestDivideValues()
        {
            var json = @"{'Dividend' : 11,'Divisor': 2}";
            var result = _calculadora.Division(JObject.Parse(json));
            var jsonResult = JObject.Parse(result);
            Assert.IsTrue(jsonResult["Quotient"] != null);
            Assert.IsTrue(jsonResult["Quotient"].ToObject<int>() == 5);
            Assert.IsTrue(jsonResult["Remainder"] != null);
            Assert.IsTrue(jsonResult["Remainder"].ToObject<int>() == 1);

        }

        [TestMethod]
        public void TestSquareRootMultipleValues()
        {
            var json = @"{'Number' : 16}";

            var result =  _calculadora.SquareRoot(JObject.Parse(json));
            var jsonResult = JObject.Parse(result);
            Assert.IsTrue(jsonResult["Square"] != null);
            Assert.IsTrue(jsonResult["Square"].ToObject<int>() == 4);

        }

    }
}

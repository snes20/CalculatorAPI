using CalculatorService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorService.Tests
{
    [TestClass]
    public class JournalQueryTests
    {
        JournalController _journal;
        CalculatorController _calculadora;


        [TestInitialize]
        public void TestInicialization()
        {
            _journal = new JournalController();
            _calculadora = new CalculatorController();

        }

        [TestMethod]
        public void QueryJournal()
        {
            var json = @"{'Addends':[5,2,3], 'Tracking-Id':20}";
            var result = _calculadora.Addition(JObject.Parse(json));

            json = "{\"Id\":20}";
            result = _journal.JournalQuery(JObject.Parse(json));

            var jsonResult = JObject.Parse(result);

            Assert.IsTrue(jsonResult["Operations"] != null);

        }


        [TestMethod]
        public void QueryJournalMultipleRecords()
        {
            var json = @"{'Addends':[5,2,3], 'Tracking-Id':20}";
             _calculadora.Addition(JObject.Parse(json));

            json = @"{'Addends':[5,26], 'Tracking-Id':20}";
            _calculadora.Addition(JObject.Parse(json));

            json = @"{'Addends':[5,9], 'Tracking-Id':20}";
            _calculadora.Addition(JObject.Parse(json));

            json = "{\"Id\":20}";
            var result = _journal.JournalQuery(JObject.Parse(json));

            var jsonResult = JObject.Parse(result);

            Assert.IsTrue(jsonResult["Operations"] != null);
            Assert.IsTrue(((JArray)jsonResult["Operations"]).Count == 3);

        }
    }
}

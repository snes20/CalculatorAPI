using CalculatorService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CalculatorService.Controllers
{
    [Produces("application/json")]
    [Route("calculator")]
    public class CalculatorController : Controller
    {
        [HttpGet]
        [Route("index")]
        public string Index()
        {
            return "Hello to the Calculator API";

        }
        
        [HttpPost]
        [Route("add")]
        public string Addition([FromBody]JObject data)
        {
            
            return AddNumbersModel.AddValues(data);

        }

        [HttpPost]
        [Route("sub")]
        public string Subtraction([FromBody]JObject data)
        {
            return SubstractValuesModel.SubtractValues(data);
        }

        [HttpPost]
        [Route("mult")]
        public string Multiply([FromBody]JObject data)
        {
            return MultiplyValuesModels.MultiplyValues(data);

        }


        [HttpPost]
        [Route("div")]
        public string Division([FromBody]JObject data)
        {
            return DivideValuesModel.DivideValues(data);
        }

        [HttpPost]
        [Route("sqrt")]
        public string SquareRoot([FromBody]JObject data)
        {
            return SquareRootValueModel.CalculateSquareRootValue(data);
        }
    }
}
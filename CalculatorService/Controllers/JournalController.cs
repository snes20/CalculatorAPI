using CalculatorService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CalculatorService.Controllers
{
    [Produces("application/json")]
    [Route("Journal")]
    public class JournalController : Controller
    {
        [HttpPost]
        [Route("query")]
        public string JournalQuery([FromBody]JObject data)
        {
            return  QueryModel.QueryJournal(data);
        }
    }
}
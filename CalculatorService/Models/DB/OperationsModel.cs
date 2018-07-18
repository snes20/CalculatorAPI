using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Models
{
    public class OperationsModel
    {
        public int Id { get; set; }
        public int JournalID { get; set; }
        public string Calculations { get; set; }
        public string Operation { get; set; }
        public DateTime StampTime { get; set; }
    }
}

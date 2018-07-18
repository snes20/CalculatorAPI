using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CalculatorService.Models
{
    public static  class DivideValuesModel
    {
        private static bool ValidJSONFormat(JObject DataToValidate)
        {
            var schema = @"{
                            'properties': {
                                'Dividend':  {'type':'integer'},
                                'Divisor':  {'type':'integer'},
                                'Tracking-Id':{ 'type': 'integer', 'optional': true}
                                }
                            }";

            var jsonSchema = JsonSchema.Parse(schema);

            return DataToValidate.IsValid(jsonSchema);

        }

        public static string DivideValues(JObject jsonObject)
        {
            string result = String.Empty;

            try
            {
                if (ValidJSONFormat(jsonObject))
                {

                    var dividend = jsonObject["Dividend"].ToObject<int>();
                    var divisor = jsonObject["Divisor"].ToObject<int>();
                    var quotient = dividend / divisor;
                    var remainder = dividend % divisor;

                    var sb = new StringBuilder($"{dividend} / {divisor} = {quotient}");

                    if (jsonObject["Tracking-Id"] != null)
                    {
                        //Add The operation to the DB
                        DBModel.Instance.InsertOperation(new OperationsModel
                        {
                            JournalID = jsonObject["Tracking-Id"].ToObject<int>(),
                            Calculations = sb.ToString(),
                            Operation = "Div",
                            StampTime = DateTime.Now
                        });
                    }

                    dynamic resultJobject = new JObject();
                    resultJobject.Quotient = quotient;
                    resultJobject.Remainder = remainder;

                    result = resultJobject.ToString();
                }
                else
                {
                    throw new OperationException(400);
                }
            }
            catch (Exception)
            {
                throw new OperationException(500);
            }

            return result;
        }
    }
}

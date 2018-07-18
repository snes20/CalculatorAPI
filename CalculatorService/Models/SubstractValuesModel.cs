using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CalculatorService.Models
{
    public class SubstractValuesModel
    {
        private static bool ValidJSONFormat(JObject DataToValidate)
        {
            var schema = @"{
                            'properties': {
                                'Minuend': {'type':'integer'},
                                'Subtrahend': {'type':'integer'},
                                'Tracking-Id':{ 'type': 'integer', 'optional': true}
                                }
                            }";
            var jsonSchema = JsonSchema.Parse(schema);

            return DataToValidate.IsValid(jsonSchema);

        }
        public static string SubtractValues(JObject jsonObject)
        {
            string result = String.Empty;

            try
            {

                if (ValidJSONFormat(jsonObject))
                {
                    //perfom Operation
                    var minuend = jsonObject["Minuend"].ToObject<int>();
                    var subtrahend = jsonObject["Subtrahend"].ToObject<int>();
                    var sb = new StringBuilder();
                    var difference = 0;

                    sb.Append($"({minuend}) - ({subtrahend})");
                    difference = minuend - subtrahend;
                    sb.Append($"={difference}");

                    if (jsonObject["Tracking-Id"] != null)
                    {
                        //Add The operation to the DB
                        DBModel.Instance.InsertOperation(new OperationsModel
                        {
                            JournalID = jsonObject["Tracking-Id"].ToObject<int>(),
                            Calculations = sb.ToString(),
                            Operation = "Subs",
                            StampTime = DateTime.Now
                        });
                    }

                    dynamic resultJobject = new JObject();
                    resultJobject.Difference = difference;

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

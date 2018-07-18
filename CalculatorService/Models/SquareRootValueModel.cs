using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CalculatorService.Models
{
    public class SquareRootValueModel
    {
        private static bool ValidJSONFormat(JObject DataToValidate)
        {
            var schema = @"{
                            'properties': {'Number': {'type':'integer'},
                                            'Tracking-Id':{ 'type': 'integer', 'optional': true}}
                            }";

            var jsonSchema = JsonSchema.Parse(schema);

            return DataToValidate.IsValid(jsonSchema);

        }
        public static string CalculateSquareRootValue(JObject jsonObject)
        {
            string result = String.Empty;

            try
            {
                if (ValidJSONFormat(jsonObject))
                {

                    var number = jsonObject["Number"].ToObject<int>();
                    var sb = new StringBuilder();
                    var squareRoot = Math.Sqrt(number);
                    sb.Append($"sqrt({number}) = {squareRoot}");

                    if (jsonObject["Tracking-Id"] != null)
                    {
                        //Add The operation to the DB
                        DBModel.Instance.InsertOperation(new OperationsModel
                        {
                            JournalID = jsonObject["Tracking-Id"].ToObject<int>(),
                            Calculations = sb.ToString(),
                            Operation = "Sqrt",
                            StampTime = DateTime.Now
                        });
                    }

                    dynamic resultJobject = new JObject();
                    resultJobject.Square = squareRoot;

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

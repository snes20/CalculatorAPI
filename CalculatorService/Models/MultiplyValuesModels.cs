using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CalculatorService.Models
{
    public class MultiplyValuesModels
    {
        private static bool ValidJSONFormat(JObject DataToValidate)
        {
            var schema = @"{
                            'properties': {'Factors':
                                {
                                  'type': 'array',
                                  'items': {'type':'integer'}
                                },
                                'Tracking-Id':{ 'type': 'integer', 'optional': true}
                            }}";
            var jsonSchema = JsonSchema.Parse(schema);

            return DataToValidate.IsValid(jsonSchema);

        }

        public static string MultiplyValues(JObject jsonObject)
        {
            string result = String.Empty;

            try
            {
                if (ValidJSONFormat(jsonObject))
                {

                    var currList = jsonObject["Factors"].ToObject<List<int>>();
                    var sb = new StringBuilder();
                    var total = 0;

                    total = currList.Aggregate((a, x) => a * x);

                    currList.ForEach(x => {
                        var nextStatement = String.IsNullOrEmpty(sb.ToString()) ? x.ToString() : $" * {x}";
                        sb.Append(nextStatement);
                    });

                    sb.Append($"={total}");

                    if (jsonObject["Tracking-Id"] != null)
                    {
                        //Add The operation to the DB
                        DBModel.Instance.InsertOperation(new OperationsModel
                        {
                            JournalID = jsonObject["Tracking-Id"].ToObject<int>(),
                            Calculations = sb.ToString(),
                            Operation = "Mul",
                            StampTime = DateTime.Now
                        });
                    }


                    dynamic resultJobject = new JObject();
                    resultJobject.Product = total;

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

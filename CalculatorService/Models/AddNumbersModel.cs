using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CalculatorService.Models
{
    public static class AddNumbersModel
    {
        private static bool ValidJSONFormat(JObject DataToValidate)
        {
            var schema = @"{
                            'properties': {'Addends':
                                {
                                  'type': 'array',
                                  'items': {'type':'integer'}
                                },
                                'Tracking-Id':{ 'type': 'integer', 'optional': true}
                            }}";
            var jsonSchema = JsonSchema.Parse(schema);

            return DataToValidate.IsValid(jsonSchema);

        }
        
        public static string AddValues(JObject jsonObject)
        {
            string result = String.Empty;


            try
            {
                if (ValidJSONFormat(jsonObject))
                {
                    //perfom Operation
                    var currList = jsonObject["Addends"].ToObject<List<int>>();
                    var sb = new StringBuilder();
                    var total = 0;

                    currList.ForEach(x =>
                    {
                        var nextStatement = String.IsNullOrEmpty(sb.ToString()) ? x.ToString() : $" + {x}";
                        sb.Append(nextStatement);
                        total += x;
                    });

                    sb.Append($"={total}");

                    if (jsonObject["Tracking-Id"] != null)
                    {
                        //Add The operation to the DB
                        DBModel.Instance.InsertOperation(new OperationsModel
                        {
                            JournalID = jsonObject["Tracking-Id"].ToObject<int>(),
                            Calculations = sb.ToString(),
                            Operation = "Sum",
                            StampTime = DateTime.Now
                        });
                    }

                    dynamic resultJobject = new JObject();
                    resultJobject.Sum = total;

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

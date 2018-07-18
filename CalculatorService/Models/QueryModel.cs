using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CalculatorService.Models
{
    public class QueryModel
    {
        private static bool ValidJSONFormat(JObject DataToValidate)
        {
            var schema = @"{
                            'properties': {'Id':{'type':'integer'}}
                            }";

            var jsonSchema = JsonSchema.Parse(schema);

            return DataToValidate.IsValid(jsonSchema);

        }

        public static string QueryJournal(JObject jsonObject)
        {
            string result = String.Empty;
            try
            {
                if (ValidJSONFormat(jsonObject))
                {
                   List<OperationsModel> data = DBModel.Instance.QueryOperation(jsonObject["Id"].ToObject<int>());
                   dynamic jsonResultObject = new JObject();
                   jsonResultObject.Operations = JArray.FromObject(data);

                   result =  JsonConvert.SerializeObject(jsonResultObject, Formatting.Indented);
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

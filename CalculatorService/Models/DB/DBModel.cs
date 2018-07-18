using NMemory;
using NMemory.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorService.Models
{
    public class DBModel:Database
    {

        static readonly DBModel _instance = new DBModel();
        private ITable<OperationsModel> Operations { get; set; }

        private DBModel()
        {
            this.Operations = Tables.Create<OperationsModel, int>(x => x.Id, new IdentitySpecification<OperationsModel>(x => x.Id, 1, 1));
        }
               
        public static DBModel Instance
        {
            get
            {
                return _instance;
            }
        }


        public void InsertOperation(OperationsModel operation)
        {
            try
            {
                _instance.Operations.Insert(operation);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<OperationsModel> QueryOperation(int JournalId)
        {
            try
            {
               return _instance.Operations.ToList().Where(x => x.JournalID == JournalId).ToList();
            }
            catch (System.Exception)
            {
                throw;
            }
        }


    }
}

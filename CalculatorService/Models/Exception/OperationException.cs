using System;

namespace CalculatorService.Models
{
    [Serializable]
    public class OperationException : System.Exception
    {
        public string ErrorCode { get; set; } = "InternalError";
        public int ErrorStatus { get; set; } 
        public string ErrorMessage { get; set; }

        public OperationException(){ }
        public OperationException(string message) : base(message) { }
        public OperationException(string message, System.Exception inner) : base(message, inner) { }
        protected OperationException(
         System.Runtime.Serialization.SerializationInfo info,
         System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public OperationException(int errorStatus)
        {
            ErrorStatus = errorStatus;

            switch (errorStatus)
            {
                case 400:
                    ErrorMessage = "Unable to process request. Please check your request";
                    break;
                case 500:
                    ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill your request. Please try again or contact support";
                    break;
            }

        }
    }

}

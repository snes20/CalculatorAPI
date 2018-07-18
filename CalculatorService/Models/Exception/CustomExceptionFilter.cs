using Easy.Logger;
using Easy.Logger.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace CalculatorService.Models
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        ILogService logService = Log4NetService.Instance;


        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = "application/json";

            var exceptionType = context.Exception.GetType();

            IEasyLogger logger = logService.GetLogger<Program>();


            context.ExceptionHandled = true;

            if (exceptionType == typeof(OperationException))
            {
                message = ((OperationException)context.Exception).ErrorMessage;
                status = (((OperationException)context.Exception).ErrorStatus == 400) ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError;

                dynamic json = new JObject();
                json.ErrorMessage = message;
                json.ErrorStatus = status;
                json.ErrorCode = ((OperationException)context.Exception).ErrorCode;

                response.StatusCode = (int)status;

                var err = ((JObject)json).ToString();

                logger.Error(err);

                response.WriteAsync(err);
            }
            else
            {

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    message = "Unauthorized Access";
                    status = HttpStatusCode.Unauthorized;
                }
                else if (exceptionType == typeof(NotImplementedException))
                {
                    message = "A server error occurred.";
                    status = HttpStatusCode.NotImplemented;
                }
                else
                {
                    message = context.Exception.Message;
                    status = HttpStatusCode.NotFound;
                }

                response.StatusCode = (int)status;
                var err = message;

                logger.Error(err);

                response.WriteAsync(err);
            }
        }
    }
}

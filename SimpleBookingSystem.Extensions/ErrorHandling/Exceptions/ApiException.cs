using System;
using System.Collections.Generic;
using System.Net;

namespace SimpleBookingSystem.Extensions.ErrorHandling.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public IDictionary<string, object> Extensions { get; set; }
        public ApiException() { }
        public ApiException(HttpStatusCode statusCode) : this(statusCode, null, null, null) { }
        public ApiException(HttpStatusCode statusCode, IDictionary<string, object> extensions = null) : this(statusCode, null, null, extensions) { }
        public ApiException(HttpStatusCode statusCode, string errorCode, IDictionary<string, object> extensions = null)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Extensions = extensions;
        }
        public ApiException(HttpStatusCode statusCode, string errorCode, string errorMessage, IDictionary<string, object> extensions = null)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}

using System;
using PingPong.Sdk.Models;

namespace PingPong.Sdk
{
    public class ApiException : Exception
    {
        public ErrorDto Error { get; }
        
        public ApiException(ErrorDto error) : base(error.Message)
        {
            Error = error;
        }

        public ApiException(int statusCode, string message, string errorCode) : this(new ErrorDto(statusCode, message, errorCode))
        {
        }

        public ApiException(ErrorDto error, Exception innerException) : base(error.Message, innerException)
        {
            Error = error;
        }
        
        public ApiException(int statusCode, string message, string errorCode, Exception innerException) 
            : this(new ErrorDto(statusCode, message, errorCode), innerException)
        {
        }
    }
}
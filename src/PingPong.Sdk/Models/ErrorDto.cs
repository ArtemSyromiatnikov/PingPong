﻿using System.Collections.Generic;
using System.Linq;

namespace PingPong.Sdk.Models
{
    public class ErrorDto
    {
        /// <summary>
        /// User-friendly error message. May be show on UI
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Machine-readable, non-localizable error code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Http Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error messages per validated field, if any
        /// </summary>
        public Dictionary<string, List<string>> ValidationErrors { get; set; }

        public bool IsValidationException => ValidationErrors?.Any() ?? false;

        public ErrorDto()
        {
        }

        public ErrorDto(int statusCode, string message, string errorCode)
        {
            StatusCode = statusCode;
            Message    = message;
            ErrorCode  = errorCode;
        }
    }
}
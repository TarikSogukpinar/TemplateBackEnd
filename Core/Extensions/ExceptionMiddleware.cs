using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;
            if (exception.GetType() == typeof(ValidationException))
            {
                message = exception.Message;
                errors = ((ValidationException) exception).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails()
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors,
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core.Tools
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context /* other scoped dependencies */)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError; // status code 500 by default

            //you can like play with this stuff and figure
            if (exception is DbUpdateException) code = HttpStatusCode.BadRequest;
			else if (exception is Exception) code = HttpStatusCode.InternalServerError;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.InternalServerError;

			var result = JsonConvert.SerializeObject(new { error = exception.Message });
			context.Response.ContentType = "application/json";
            //todo: sometimes the StatuCode will be read-only and throw an Exception - need to fix it
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(result);
		}
	}
}

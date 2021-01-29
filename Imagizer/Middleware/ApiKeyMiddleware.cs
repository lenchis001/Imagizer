using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Imagizer.Middleware
{
	public class ApiKeyMiddleware
	{
		private readonly string API_KEY = "ApiKey";

		private readonly RequestDelegate _next;
		private readonly string _apiKey;

		public ApiKeyMiddleware(RequestDelegate next)
		{
			_next = next;

			_apiKey = Environment.GetEnvironmentVariable(API_KEY);
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Method != HttpMethods.Get &&
				context.Request.Headers[API_KEY] != _apiKey)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			}
			else
			{
				await _next(context);
			}
		}
	}
}

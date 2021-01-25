using Imagizer.Models.API.V1;
using Microsoft.AspNetCore.Mvc;

namespace Imagizer.Controllers.API.V1
{
	public static class ControllerExtensions
	{
		public static ActionResult ToActionResult(this Controller controller, ErrorCode errorCode)
		{
			switch (errorCode)
			{
				case ErrorCode.OK:
					return controller.Ok();
				case ErrorCode.UNKNOWN:
					return controller.Problem();
				case ErrorCode.ACCESS_DENIED:
					return controller.Unauthorized();
				default:
					return controller.Problem();
			}
		}

		public static ActionResult<T> ToActionResult<T>(this Controller controller, ErrorCode errorCode, T successData)
		{
			switch (errorCode)
			{
				case ErrorCode.OK:
					return successData;
				default:
					return controller.ToActionResult(errorCode);
			}
		}
	}
}

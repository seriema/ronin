using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;


namespace Ronin.Filters
{
	public class ValidateHttpAntiForgeryTokenAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			HttpRequestMessage request = actionContext.ControllerContext.Request;
			try
			{
				if (IsAjaxRequest(request))
				{
					ValidateRequestHeader(request);
				}
				else
				{
					AntiForgery.Validate();
				}
			}
			catch (HttpAntiForgeryException e)
			{
				actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
			}
		}
		private bool IsAjaxRequest(HttpRequestMessage request)
		{
			IEnumerable<string> xRequestedWithHeaders;
			if (request.Headers.TryGetValues("X-Requested-With", out xRequestedWithHeaders))
			{
				string headerValue = xRequestedWithHeaders.FirstOrDefault();
				if (!String.IsNullOrEmpty(headerValue))
				{
					return String.Equals(headerValue, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
				}
			}


			return false;
		}


		private void ValidateRequestHeader(HttpRequestMessage request)
		{
			string cookieToken = string.Empty;
			var firstOrDefault = request.Headers.GetCookies().Select(c => c[AntiForgeryConfig.CookieName]).FirstOrDefault();
			if (firstOrDefault != null)
				cookieToken = firstOrDefault.Value;

			string formToken = String.Empty;
			IEnumerable<string> tokenHeaders;
			if (request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
			{
				string tokenValue = tokenHeaders.FirstOrDefault();
				if (!String.IsNullOrEmpty(tokenValue))
					formToken = tokenValue.Trim();
			}


			AntiForgery.Validate(cookieToken, formToken);
		}
	}
}
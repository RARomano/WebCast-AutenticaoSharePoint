using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCast.AutenticacaoSharePoint.Controllers
{
	public class HomeController : Controller
	{
		/// Url copiada do pontos de entrada, somente até o guid após login.microsoftonline.com
		/// exemplo: https://login.microsoftonline.com/B1EC3377-86A0-43EE-8305-FE1B1B3AE270
		string authority = "";

		/// Url do Tenant que deseja acessar
		string resource = "";

		/// Client ID
		string clientId = "";

		/// Client Secret
		string clientSecret = "";

		/// Url de Redirect
		string redirectUrl = "http://localhost:58689/Home/Token";

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Token(string code)
		{
			var authenticationContext = new AuthenticationContext(authority);
			var tokenResult = authenticationContext.AcquireTokenByAuthorizationCode(code, new Uri(redirectUrl), new ClientCredential(clientId, clientSecret), resource);

			var accessToken = tokenResult.AccessToken;


			using (ClientContext ctx = new ClientContext(resource))
			{
				ctx.ExecutingWebRequest += (object sender, WebRequestEventArgs e) =>
				{
					e.WebRequestExecutor.RequestHeaders.Add("Authorization", "Bearer " + accessToken);
				};

				var web = ctx.Web;
				ctx.Load(web, a => a.Title, a=>a.CurrentUser);
				ctx.ExecuteQuery();

				var title = web.Title;

				ViewBag.SiteTitle = title;
				ViewBag.LoggedUser = web.CurrentUser.Title;
				ViewBag.LoggedUserEmail = web.CurrentUser.Email;
			}

			return View("Index");
		}





		public ActionResult Conectar()
		{
			var authenticationContext = new AuthenticationContext(authority);

			var url = authenticationContext.GetAuthorizationRequestURL(resource, clientId, new Uri(redirectUrl), UserIdentifier.AnyUser, null);

			//var accessToken = authenticationContext.(resource, new ClientCredential(clientId, clientSecret)).AccessToken;

			return Redirect(url.ToString());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
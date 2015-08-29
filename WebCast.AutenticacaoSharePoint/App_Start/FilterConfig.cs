using System.Web;
using System.Web.Mvc;

namespace WebCast.AutenticacaoSharePoint
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}

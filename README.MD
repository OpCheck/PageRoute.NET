# PageRoute.NET

The zero-configuration URL to endpoint routing system for ASP.NET.


	<%@Page Inherits="GSR.Dashboard.Web.EndPointRouter" %>

	
	public class AppRouter : PageRouter.Router
	{
		public AppRouter : base("GSR.Dashboard.Web")
		{}
	}


	using PageRoute;

	namespace GSR.Dashboard.Web
	{
		public class CompanyValueGaugeEndpoint : Endpoint
		{
			public override void Execute ()
			{
				Response.Status = 200;
				Response.ContentType = "application/json";
			}
		}
	}

using PageRoute;

namespace PageRouteDemoApp
{
	public class GetOnlyEndpoint : Endpoint
	{
		public void Get ()
		{
			Response.ContentType = "text/plain";
			Response.Write("IT WORKS! - PageRouteDemoApp.GetOnlyEndpoint.Get()");
		}
	}
}

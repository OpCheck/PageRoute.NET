using PageRoute;

namespace PageRouteDemoApp
{
	public class PostOnlyEndpoint : Endpoint
	{
		public void Post ()
		{
			Response.ContentType = "text/plain";
			Response.Write("IT WORKS! - PageRouteDemoApp.PostOnlyEndpoint.Post()");
		}
	}
}

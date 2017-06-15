using PageRoute;

namespace PageRouteDemoApp
{
	public class TestEndpoint : Endpoint
	{
		public override void Execute()
		{
			Response.ContentType = "text/plain";
			Response.Write("IT WORKS! - PageRouteDemoApp.TestEndpoint.Execute()");
		}
	}
}

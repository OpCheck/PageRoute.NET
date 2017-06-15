using System.Web;


namespace PageRoute
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class Endpoint
	{
		public abstract void Execute ();
		
		
		public HttpApplicationState Application;
		public HttpRequest Request;
		public HttpResponse Response;
		public string EndpointCode;
	}
}

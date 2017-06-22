using System.Web;
using System.Web.UI;

namespace PageRoute
{
	/// <summary>
	/// Contains the underlying infrastructure such as intrinsic page objects.
	/// Derived classes contain the application logic that is mapped to by the URL.
	/// </summary>
	public abstract class Endpoint
	{
		public HttpApplicationState Application
		{
			get
			{
				return Page.Application;
			}
		}

		
		public HttpRequest Request
		{
			get
			{
				return Page.Request;
			}
		}


		public HttpResponse Response
		{
			get
			{
				return Page.Response;
			}
		}


		public string EndpointCode;
		public Page Page;
	}
}

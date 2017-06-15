using System;
using System.Web.UI;

namespace PageRoute
{
	/// <summary>
	/// The page that all ASPX or other physical files inherit from for page routing.
	/// For short, we just call this page the "router."
	/// Your app router must inherit from this and specify an assembly and/or namespace where the endpoints are to be found.
	/// </summary>
	public abstract class RouterPage : Page
	{
		/// <summary>
		/// Create a new instance of the router with setting the endpoint location properties.
		/// Use this constructor if you intend to set these properties in your app router's constructor body.
		/// </summary>
		protected RouterPage () : base()
		{
			Load += RouterPage_Load;
		}
	
	
		/// <summary>
		/// Create a new instance of the router, setting the endpoint location properties.
		/// </summary>
		protected RouterPage (string AssemblyName, string EndpointNamespace) : base()
		{
			_EndpointAssemblyName = AssemblyName;
			_EndpointNamespace = EndpointNamespace;

			Load += RouterPage_Load;
		}
		
		
		protected void RouterPage_Load (object Sender, EventArgs Args)
		{
			//
			// GET THE URL.
			// CHOP OFF THE EXTENSION.
			//
			// http://server/Endpoints/CompanyValueGauge.aspx
			//
			
			//
			// GET THE PATH.
			//
			
			//
			// GET THE ENDPOINT CODE.
			//
			string EndpointCode = "Test";//EndpointCodeExtractor.GetEndpointCodeFromPath(Path);
			
			//
			// CREATE THE ENDPOINT.
			//
			EndpointFactory CreatedFactory = new EndpointFactory();
			CreatedFactory.AssemblyName = _EndpointAssemblyName;
			CreatedFactory.Namespace = _EndpointNamespace;
			CreatedFactory.EndpointCode = EndpointCode;
			Endpoint CreatedEndpoint = CreatedFactory.CreateEndPoint();
			
			//
			// CONFIGURE THE ENDPOINT.
			//
			CreatedEndpoint.Application = Application;
			CreatedEndpoint.Request = Request;
			CreatedEndpoint.Response = Response;
			CreatedEndpoint.EndpointCode = EndpointCode;
			
			//
			// EXECUTE THE ENDPOINT.
			//
			CreatedEndpoint.Execute();
		}
		
		
		protected string _EndpointAssemblyName;
		protected string _EndpointNamespace;
	}
}

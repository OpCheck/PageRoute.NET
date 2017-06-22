using System;
using System.Reflection;
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

			//
			// ADD THE LOAD EVENT.
			//
			Load += RouterPage_Load;
		}
		
		
		protected void RouterPage_Load (object Sender, EventArgs Args)
		{
			//
			// GET THE ENDPOINT CODE BY SPLITTING THE URL AND CHOPPING OFF THE EXTENSION.
			//
			string EndpointCode = EndpointCodeExtractor.GetEndpointCodeFromPath(Request.FilePath);
			
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
			CreatedEndpoint.Page = this;
			
			//
			// DETERMINE THE METHOD NAME TO CALL.
			//
			string MethodName = Char.ToUpper(Request.HttpMethod[0]) + Request.HttpMethod.ToLower().Substring(1, Request.HttpMethod.Length - 1);
			
			//
			// GET THE TYPE OF THE CREATED ENDPOINT.
			// WE WILL USE THIS MULTIPLE TIMES TO REFLECT AGAINST.
			//
			Type CreatedEndpointType = CreatedEndpoint.GetType();

			//
			// CHECK IF THE ENDPOINT IMPLEMENTS THE TARGET METHOD.
			//
			MethodInfo TargetMethod = CreatedEndpointType.GetMethod(MethodName);
			
			if (TargetMethod != null)
			{
				//
				// EXECUTE THE TARGET METHOD.
				//
				TargetMethod.Invoke(CreatedEndpoint, new object[]{});
				return;
			}
			
			//
			// THE TARGET METHOD IS NOT IMPLEMENTED.
			// CHECK FOR THE DEFAULT EXECUTE METHOD.
			//
			MethodInfo ExecuteMethod = CreatedEndpointType.GetMethod("Execute");

			if (ExecuteMethod != null)
			{
				//
				// EXECUTE THE "EXECUTE" METHOD ON THE ENDPOINT.
				//
				ExecuteMethod.Invoke(CreatedEndpoint, new object[]{});
				return;
			}
			
			//
			// NO DEFAULT EXECUTE METHOD FOUND EITHER.
			// RETURN AN HTTP 404 ERROR WITH NO CONTENT.
			//
			Response.StatusCode = 405;
			Response.SuppressContent = true;
		}
		
		
		protected string _EndpointAssemblyName;
		protected string _EndpointNamespace;
	}
}

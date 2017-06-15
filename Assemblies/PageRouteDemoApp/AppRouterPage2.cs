using PageRoute;

namespace PageRouteDemoApp
{
	/// <summary>
	/// A sample app router.
	/// This sample demonstrates the setter-style syntax that is available when writing an app router.
	/// </summary>
	public class AppRouterPage2 : RouterPage
	{
		/// <summary>
		/// Creates a new instance of this app router, specifying the namespace where the endpoints reside.
		/// This is a more descriptive but requires knowledge of the field name and more typing.
		/// </summary>
		public AppRouterPage2 () : base()
		{
			_EndpointAssemblyName = "PageRouteDemoApp";
			_EndpointNamespace = "PageRouteDemoApp";
		}
	}
}

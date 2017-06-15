using PageRoute;

namespace PageRouteDemoApp
{
	/// <summary>
	/// A sample app router.
	/// This sample demonstrates the constructor-style syntax that is available.
	/// </summary>
	public class AppRouterPage1 : RouterPage
	{
		/// <summary>
		/// Creates a new instance of this app router, specifying the namespace where the endpoints reside.
		/// This is a nice, terse way of writing this but is less descriptive than the other example.
		/// </summary>
		public AppRouterPage1 () : base("PageRouteDemoApp", "PageRouteDemoApp")
		{}
	}
}

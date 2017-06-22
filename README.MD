# PageRoute.NET

An ASP.NET framework that maps a page URL (ending in *.aspx) to an endpoint implementation with absolutely no configuration required.
For each page URL, a web service implementer need only concern himself with writing an endpoint class.
The result is an extremely simple and portable web service implementation that anyone can understand.

## Motivation

Some people believe that going to great lengths to remove the file extension('.aspx') from the URL is worth the effort.
We don't, because it requires a more complex configuration.

A more complex configuration has disadvantages for everyone - both software developers and system administrators.

Once you have a configuration that works, then you have to document the process - when you first write it and every time you change it.
This takes time.

On the other end, following a configuration process for an ASP.NET application can be an error-prone and very time-consuming process.
For developers, setting up a new local server can be an excruciatingly-long exercise in trial-and-error.

Administrators suffer from the same problems that developers do - but they also get to suffer the consequences of setting up a wrongly-configured server in a production scenario.

One of the goals of PageRoute is to avoid all of this by embracing the default configurations of IIS and ASP.NET.

## Advantages

1. No modifications to the web.config file are needed.

1. No configuration to IIS is needed if the default ASP.NET handler mappings are installed.

1. This framework has no configuration file.

1. The web application directory structure defines which URLs are valid.  You can easily tell which ASPX files exist and consequently know which 

1. A routed ASPX page can reside in any directory in the web application and can be moved around without restriction.  It always maps to the same endpoint class.

## Trade-offs

Consider the following resource URL style:

	http://127.0.0.1/Company/100

1. The resource URL style is short - so it's easier to construct by hand and code for.
However, this style requires additional configuration and is less descriptive.
What does the 100 mean?

1. The resource URL style has a definite parameter order.
This is useful for caching, permalinking and many other practices but is also less flexible.

Now consider the **ASPX URL** equivalent:

	http://127.0.0.1/Company.aspx?CompanyId=100
	
1. The file-based URL style is longer - but it works with no additional configuration.

1. The file-based URL style is more descriptive.
We can determine what the intent of the request is just by looking at it.

1. The file-based URL style allows parameters to appear in any order.
This allows considerable flexibility when crafting a request.

## Terminology

An **ASPX URL** is any file-based URL that refers a physical ASPX page such as 'http://127.0.0.1/MyPage.aspx'.

An **endpoint** is a class that implements the web service logic that we ultimately execute.

A **router** is a special page class that analyzes the ASPX URL path and executes an endpoint.
A single router instance always looks for endpoints in the same assembly and namespace.

## Getting Started

Here is how to build a routed ASPX page using PageRoute.NET.

Let's say we are building a finance web application.
We get company data by hitting the following URL:

	http://127.0.0.1/Finance/Company.aspx?CompanyId=100

The contents of 'CompanyData.aspx' is simply this:

	<%@Page Inherits="Finance.AppRouterPage" %>

This page only exists so the URL is valid.
Every page inherits from a router.
A router is just a special page that finds the endpoint to execute.

Define the router as follows:

	using PageRoute;

	namespace Finance
	{
		public class AppRouterPage : RouterPage
		{
			public AppRouterPage () : base("FinanceAssembly", "Finance")
			{}
		}
	}

The only thing we are expressing here is that this router looks for endpoints in assembly 'FinanceAssembly.dll' and 'Finance' namespace.

Here's how the router determines which endpoint it's looking for in this example:

1. Get the file path.  Result: '/Finance/Company.aspx'.

1. Get the name of the ASPX file.  Result: 'Company.aspx'.

1. Chop off the extension:  Result: 'Company'. 

1. Append the word 'Endpoint'.  Result: 'CompanyEndpoint'.  The router now has the endpoint class name that it needs.

1. The router already knows which assembly and namespace to look in to find this class.

Define your endpoint as follows:

	using PageRoute;

	namespace Finance
	{
		public class CompanyEndpoint : Endpoint
		{
			public void Execute ()
			{
				Response.Status = 200;
				Response.ContentType = "text/plain";
				Response.Write("IT WORKS!  CompanyEndpoint.Execute().");
			}
		}
	}

The router creates an instance of 'CompanyEndpoint' and invokes the 'Execute' method.

We are done.  This completes the example.

## Intrinsic Objects

You may have noticed that we are using the intrinsic 'Response' property just like we would on any ASP.NET page.
Endpoints act as a facade and expose the exact same intrinsic objects.

In the case that we have missed any, we also expose the page itself as a property - so you can always write 'Page.Response' instead.

## Routers

A router defines a set of routed URLs where each routed URL maps to an endpoint in the same assembly and namespace.
A router is just an ASP.NET page itself.
You can define as many routers as you wish.
This is useful if you have endpoint classes that reside in different assemblies or namespaces.

## Restricting the HTTP Method

Instead of implementing 'Execute' - which will serve any request - you can write:

	using PageRoute;

	namespace Finance
	{
		public class CompanyEndpoint : Endpoint
		{
			public void Get ()
			{
				Response.Status = 200;
				Response.ContentType = "text/plain";
				Response.Write("IT WORKS!  CompanyEndpoint.Execute().");
			}
		}
	}

This restricts the endpoint to only serve HTTP GET requests.

If an HTTP POST request is sent to the 'Company.aspx' URL then the router would return an HTTP status code of 404 with no content.
The endpoint would be instantiated but no methods would be invoked.

You can also implement the 'Execute' method to handle .
In this case all HTTP GET requests would get routed to the 'Get' method and everything else would get routed to the 'Execute' method.

## Router Algorithm



## Additional Notes

This framework does not implement a Model-View-Controller (MVC) design pattern.
MVC is appropriate for several applications and has its place.
While all MVC frameworks in practice also need a routing system to determine which controller to execute - here we are only solving the problem of mapping HTTP requests to endpoint logic.

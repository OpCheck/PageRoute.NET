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

## Trade-off Analysis

A lot of web service developers prefer resource URLs such as this one:

	http://127.0.0.1/Company/100

That's an understandable viewpoint because:

1. the above URL is short.  This makes it easier to construct by hand.

2. the above URL has a definite parameter order.  This is useful for caching, permalinking and many other practices.

However, this style requires additional configuration and is less self-descriptive.

Now consider the ASPX file equivalent of the previous URL:

	http://127.0.0.1/Company.aspx?CompanyId=100
	
This is a completely understandable, self-descriptive URL that works with no additional configuration.

This style is longer and allows parameters to appear in any order.

## Terminology

A page URL is any URL that contains an ASPX page such as 'http://127.0.0.1/MyPage.aspx'.

An endpoint is a class that implements some web application logic.

An endpoint group is a set of endpoint classes that reside in the same assembly and namespace.

A route maps a page URL to an endpoint.

A router is a class that defines a set of routes.

## Getting Started

Here is how to build a routed ASPX page using PageRoute.NET.
Let's say we are building a finance web application that has a company data endpoint at the following URL:

	http://127.0.0.1/Finance/Company.aspx?CompanyId=100

The contents of the CompanyData.aspx is this:

	<%@Page Inherits="Finance.AppRouterPage" %>
	
The only thing we are expressing here is that this page's URL is going to be processed by the finance app's router page.
The router page defines a set of routes.
A route maps a URL to an endpoint class.
Every routed ASPX page in the finance app is exactly the same.

Define the finance app's router as follows:

	using PageRoute;

	namespace Finance
	{
		public class AppRouterPage : RouterPage
		{
			public AppRouterPage1 () : base("FinanceAssembly", "Finance")
			{}
		}
	}

What we are saying above is that this router searches for endpoints in assembly 'FinanceAssembly.dll' and 'Finance' namespace.
The assembly name is the first parameter and the namespace is the second parameter.
This is how endpoints are grouped.
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

This completes the example.
The URL is now mapped to the 'CompanyEndpoint' class.

## Intrinsic Objects

You will have likely noticed that we are using the intrinsic 'Response' object just like we would on a normal page.
Endpoints have all of the same intrinsic objects available.
In the case that we have missed any, you can always write 'Page.Response' instead.

## Routers

A router defines a set of routed URLs where each routed URL maps to an endpoint in the same assembly and namespace.
A router is just an ASP.NET page itself.
You can define as many routers as you wish.
This is useful if you have endpoint classes that reside in different assemblies or namespaces.

## Method

Instead of implementing method 'Execute' you can write:

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
If someone tried to post to it then the router would return an HTTP status code of 404 with no content.
You can also implement the 'Execute' method.
In this case all HTTP GET requests would get routed to the 'Get' method and everything else would get routed to the 'Execute' method.

## Additional Notes

This framework does not implement a Model-View-Controller (MVC) design pattern.
MVC is appropriate for several applications and has its place.
While all MVC frameworks in practice also need a routing system to determine which controller to execute - here we are only solving the problem of mapping HTTP requests to endpoint logic.

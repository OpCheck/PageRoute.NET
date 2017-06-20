# PageRoute.NET

The zero-configuration URL to endpoint logic routing system for ASP.NET.

## Terminology

A page URL is any URL that contains an ASPX page such as 'http://127.0.0.1/MyPage.aspx'.

An endpoint is a class that implements some web application logic.

A route maps a page URL to an endpoint.

A router is a class that defines a set of routes.

## About

This is an ASP.NET framework that maps a page URL (*.aspx) to an endpoint implementation without changing the default IIS web server configuration.
For each URL, a web service implementer need only concern himself with writing an endpoint class.
The result is an extremely simple and portable web service implementation that anyone can understand.

This framework does not implement an Model-View-Controller (MVC) design pattern.
MVC is appropriate for several applications and has its place.
While all MVC frameworks in practice also need a routing system to determine which controller to execute - here we are only solving the problem of mapping HTTP requests to endpoint logic.

## Motivation

A lot of web service developers like nice, terse, resource URLs such as this:

	http://127.0.0.1/Company/100
	
However, in order to achieve that you have to edit the web.config file and add an HTTP handler.
Also, the above URL style is not very descriptive.

Now consider the ASPX equivalent:

	http://127.0.0.1/Company.aspx?CompanyId=100
	
Is that so bad?  This is a completely understandable URL.
I believe that this obsession with removing the '.aspx' from the URL is not worth the effort because it requires a more complex web configuration.

A complex configuration has disadvantages for everyone - both software developers and system administrators.
Once you have a configuration that works, then you have to document it - when you first write it and every time the configuration changes - so other people can follow it.  This takes time.

On the other end, following a configuration for an ASP.NET application can be an error-prone and very time-consuming process.
Setting up a new server for local development has been a long exercise in trial-and-error - and has caused productivity issues - for this author.
Setting up a wrongly-configured server in a production scenario can have even worse consequences.

One of the goals of PageRoute is to avoid all of this by embracing the default configurations of IIS and ASP.NET.

## Advantages

1. No modifications to the web.config file are needed.

1. No configuration to IIS is needed if the default ASP.NET handler mappings are installed.

1. This framework has no configuration file.

1. The web application directory structure defines which URLs are valid.  You can easily tell which ASPX files exist and therefore 

1. A routed ASPX page can reside in any directory in the web application and can be moved around without restriction.  It always maps to the same endpoint class.

## Disadvantages

1. This is a page URL-based framework that relies on the physical ASPX f

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

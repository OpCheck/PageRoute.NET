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

One of the goals of PageRoute is to avoid all of this by embrace the default configuration of IIS and ASP.NET.

The author of this library has noticed a trend where 

## Advantages

1. 

1. No modifications to the web.config file are needed.

1. No additional configuration to IIS is needed if the default ASP.NET handler mappings are installed.

## Disadvantages

1. 


## Getting Started

Let's say we are building a finance app with a company data endpoint at the following URL:

	http://127.0.0.1/FinanceApp/CompanyData.aspx?CompanyId=100

The contents of the CompanyData.aspx is this:

	<%@Page Inherits="FinanceApp.AppRouterPage" %>

Every page is exactly the same.

Define your app router as follows:

	namespace FinanceApp
	{
		public class AppRouterPage : PageRoute.RouterPage
		{
			public AppRouterPage1 () : base("FinanceApp", "FinanceApp")
			{}
		}
	}

Define your endpoint as follows:

	namespace FinanceApp
	{
		public class CompanyDataEndpoint : PageRoute.Endpoint
		{
			public override void Execute ()
			{
				Response.Status = 200;
				Response.ContentType = "text/plain";
				Response.Write("CompanyDataEndpoint");
			}
		}
	}

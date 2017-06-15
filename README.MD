# PageRoute.NET

The zero-configuration URL to endpoint logic routing system for ASP.NET.

## About

This framework does not implement MVC.

It strictly solves the problem of mapping HTTP requests to application logic.

A route is 

Instead of , we instead embrace the default configuration of ASP.NET.

## Motivation

Configuration of IIS for ASP.NET applications can be a time-consuming and error-prone process.

Once you have a configuration that works, it is important to document the configuration.

The goal is avoid all of that by embracing the working, default configuration that IIS offers us.

The traditional ASP.NET event model is fading away with frameworks such as ASP.NET, Web API, etc.

These frameworks allow you to write 

## Advantages

1. 

1. No modifications to the web.config file are needed.

1. No additional configuration to IIS is needed.

## Disadvantages



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

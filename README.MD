# PageRoute.NET

The zero-configuration URL to endpoint logic routing system for ASP.NET.

## About

This framework does not implement MVC.

It strictly solves the problem of mapping HTTP requests to application logic.

A route is 

Instead of , we instead embrace the default configuration of ASP.NET.

## Motivation

A complex IIS configuration has disadvantages for everyone - both software developers and system administrators.
Once you have a configuration that works, then you have to document it - when you first write it and every time the configuration changes - so other people can follow it.  This takes time.

On the other end, following a configuration of IIS for an ASP.NET application can be an error-prone and very time-consuming process.
Setting up a new server for local development has been an exercise in trial-and-error.
Setting up a server for production 

One of the goals of PageRoute is to avoid all of this.

We embrace the working, default configuration that IIS offers us.


There is no concept of model, view, controller.  
I became frustrated with the

You can write good-looking, resource URIs using ASP.NET MVC and Web API - but the value here is in their routing systems.
These frameworks allow you to write 
You can 

The goal is 

The traditional ASP.NET event model long faded away with frameworks such as ASP.NET, Web API, and other.


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

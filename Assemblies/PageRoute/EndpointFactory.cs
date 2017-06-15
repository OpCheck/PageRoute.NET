using System;

namespace PageRoute
{
	public class EndpointFactory
	{
		public Endpoint CreateEndPoint ()
		{
			return (Endpoint)Activator.CreateInstance(_AssemblyName, String.Format("{0}.{1}Endpoint", _Namespace, _EndpointCode)).Unwrap();
		}


		public string AssemblyName
		{
			set
			{
				_AssemblyName = value;
			}
		}
	
	
		public string Namespace
		{
			set
			{
				_Namespace = value;
			}
		}


		public string EndpointCode
		{
			set
			{
				_EndpointCode = value;
			}
		}
		
		
		private string _AssemblyName;
		private string _Namespace;
		private string _EndpointCode;
	}
}

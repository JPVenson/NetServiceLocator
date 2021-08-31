using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Attributes
{
	/// <summary>
	///		Declares a new Service that should be imported as a Singleton Service
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class SingletonServiceAttribute : ServiceAttribute
	{
		/// <summary>
		///		 
		/// </summary>
		/// <param name="registerAs">Defines all interfaces that this service should be available</param>
		public SingletonServiceAttribute(params Type[] registerAs) : base(registerAs)
		{
			RegistrationType = ServiceLifetime.Singleton;
		}
	}
}

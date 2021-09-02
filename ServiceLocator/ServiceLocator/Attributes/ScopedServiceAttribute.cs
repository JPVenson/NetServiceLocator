using System;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Attributes
{
	/// <summary>
	///		Declares a new Service that should be imported as a <see cref="ServiceLifetime.Scoped"/> service
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class ScopedServiceAttribute : ServiceAttribute
	{
		/// <summary>
		///		 
		/// </summary>
		/// <param name="registerAs">Defines all interfaces that this service should be available</param>
		public ScopedServiceAttribute(params Type[] registerAs) : base(registerAs)
		{
			RegistrationType = ServiceLifetime.Scoped;
		}
	}
}
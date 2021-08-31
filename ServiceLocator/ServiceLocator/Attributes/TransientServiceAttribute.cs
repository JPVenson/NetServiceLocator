using System;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Attributes
{
	/// <summary>
	///		Declares a new Service that should be imported as a Scoped Service
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class TransientServiceAttribute : ServiceAttribute
	{
		/// <summary>
		///		 
		/// </summary>
		/// <param name="registerAs">Defines all interfaces that this service should be available</param>
		public TransientServiceAttribute(params Type[] registerAs) : base(registerAs)
		{
			RegistrationType = ServiceLifetime.Transient;
		}
	}
}
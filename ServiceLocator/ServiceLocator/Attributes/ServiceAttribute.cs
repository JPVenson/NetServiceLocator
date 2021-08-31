using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Attributes
{
	/// <summary>
	///		Registers a new Service that should be imported by using the <see cref="RegistrationType"/>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class ServiceAttribute : Attribute
	{
		/// <summary>
		///		Imports the declaring type and all <see cref="RegisterAs"/> as Service types
		/// </summary>
		/// <param name="registerAs"></param>
		public ServiceAttribute(params Type[] registerAs)
		{
			RegisterAs = registerAs;
		}

		/// <summary>
		///		Gets or sets the list of Service Types the declaring type should be registered with
		/// </summary>
		public IEnumerable<Type> RegisterAs { get; set; }

		/// <summary>
		///		Gets or sets the <see cref="ServiceLifetime"/> the declaring type should be registered with
		/// </summary>
		public ServiceLifetime RegistrationType { get; set; }

		internal IEnumerable<ServiceDescriptor> GetDescriptors(Type declaringType)
		{
			return GetRegistrations(declaringType);
		}

		/// <summary>
		///		Enumerates all <see cref="ServiceDescriptor"/> for the <see cref="declaringType"/>
		/// </summary>
		/// <param name="declaringType"></param>
		/// <returns></returns>
		protected virtual IEnumerable<ServiceDescriptor> GetRegistrations(Type declaringType)
		{
			yield return new ServiceDescriptor(declaringType, declaringType, RegistrationType);
			foreach (var registerA in RegisterAs)
			{
				yield return new ServiceDescriptor(registerA, e => e.GetService(declaringType), RegistrationType);
			}
		}
	}
}
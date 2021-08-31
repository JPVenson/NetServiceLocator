using System;
using System.Collections.Generic;
using System.Reflection;
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
			var serviceType = declaringType;
			yield return new ServiceDescriptor(serviceType, serviceType, RegistrationType);
			foreach (var registerA in RegisterAs)
			{
				yield return new ServiceDescriptor(registerA, e => e.GetService(serviceType), RegistrationType);
			}

			var autoReg = serviceType.GetCustomAttribute<WithAutoRegistrationAttribute>();
			if (autoReg != null)
			{
				if (autoReg.RegistrationKind.HasFlag(RegistrationKind.Interfaces))
				{
					foreach (var @interface in serviceType.GetInterfaces())
					{
						yield return new ServiceDescriptor(@interface, e => e.GetService(serviceType), RegistrationType);
					}
				}

				if (autoReg.RegistrationKind.HasFlag(RegistrationKind.BaseClasses))
				{
					while (declaringType.BaseType != null && declaringType.BaseType != typeof(object))
					{
						yield return new ServiceDescriptor(declaringType.BaseType, e => e.GetService(serviceType), RegistrationType);

						if (autoReg.RegistrationKind.HasFlag(RegistrationKind.Interfaces))
						{
							foreach (var @interface in declaringType.GetInterfaces())
							{
								yield return new ServiceDescriptor(@interface, e => e.GetService(serviceType), RegistrationType);
							}
						}
						declaringType = declaringType.BaseType;
					}
				}
			}
		}
	}

	/// <summary>
	///		If used with <see cref="ServiceAttribute"/> allows to automatically register all used interfaces or base classes 
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class WithAutoRegistrationAttribute : Attribute
	{
		public WithAutoRegistrationAttribute()
		{
			RegistrationKind = RegistrationKind.BaseClasses | RegistrationKind.Interfaces;
		}

		public RegistrationKind RegistrationKind { get; set; }
	}

	[Flags]
	public enum RegistrationKind
	{
		All = BaseClasses | Interfaces,
		BaseClasses = 1 << 0,
		Interfaces = 1 << 1
	}
}
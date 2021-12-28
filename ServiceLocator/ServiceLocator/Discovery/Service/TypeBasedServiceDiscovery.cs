using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator.Attributes;

namespace ServiceLocator.Discovery.Service
{
	/// <summary>
	///		Can be used as a base class that works with Types annotated by a <see cref="ServiceAttribute"/>
	/// </summary>
	public abstract class TypeBasedServiceDiscovery : IServiceDiscovery
	{
		/// <summary>
		///		Provides a list of types that are checked for <see cref="ServiceAttribute"/>
		/// </summary>
		/// <returns></returns>
		protected abstract IEnumerable<Type> DiscoverTypes();

		/// <inheritdoc />
		public IEnumerable<ServiceDescriptor> DiscoverServices(IServiceDiscoveryManager locator)
		{
			return DiscoverTypes()
				.Where(ServiceAttribute.HasServiceDescriptor)
				.SelectMany(e => e.GetCustomAttribute<ServiceAttribute>(true).GetDescriptors(e));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator.Attributes;

namespace ServiceLocator.Discovery.Option
{
	/// <summary>
	///		Can be used as a base class that works with Types annotated by a <see cref="ServiceAttribute"/>
	/// </summary>
	public abstract class TypeBasedOptionDiscovery : IServiceDiscovery
	{
		private readonly IConfiguration _configuration;

		protected TypeBasedOptionDiscovery(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		///		Provides a list of types that are checked for <see cref="ServiceAttribute"/>
		/// </summary>
		/// <returns></returns>
		protected abstract IEnumerable<Type> DiscoverTypes();

		/// <inheritdoc />
		public IEnumerable<ServiceDescriptor> DiscoverServices(IServiceDiscoveryManager locator)
		{
			return DiscoverTypes()
				.Where(FromConfigAttribute.HasConfigDescriptor)
				.SelectMany(e => e.GetCustomAttribute<FromConfigAttribute>(true).GetOptionDescriptors(e, _configuration));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator.Attributes;

namespace ServiceLocator.Discovery
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceDiscoveryManager : IServiceDiscoveryManager
	{
		/// <inheritdoc />
		public IServiceCollection ServiceCollection { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serviceCollection"></param>
		public ServiceDiscoveryManager(IServiceCollection serviceCollection)
		{
			ServiceCollection = serviceCollection;
			_discoveries = new List<IServiceDiscovery>();
		}

		private readonly IList<IServiceDiscovery> _discoveries;
		
		/// <inheritdoc />
		public IEnumerable<Type> DiscoverTypes()
		{
			return _discoveries.SelectMany(e => e.DiscoverTypes(FilterType));
		}

		private bool FilterType(Type arg)
		{
			return arg.GetCustomAttribute<ServiceAttribute>(false) != null;
		}
		
		/// <inheritdoc />
		public void AddDiscovery(IServiceDiscovery discovery)
		{
			_discoveries.Add(discovery);
		}
	}
}
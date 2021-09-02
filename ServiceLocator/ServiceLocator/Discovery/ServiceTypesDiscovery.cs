using System;
using System.Collections.Generic;
using ServiceLocator.Discovery.Options;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Loads all types that are set in its constructor
	/// </summary>
	public class ServiceTypesDiscovery : TypeBasedServiceDiscovery
	{
		private readonly IEnumerable<Type> _types;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="types"></param>
		public ServiceTypesDiscovery(IEnumerable<Type> types)
		{
			_types = types;
		}

		/// <inheritdoc />
		protected override IEnumerable<Type> DiscoverTypes()
		{
			return _types;
		}
	}
}
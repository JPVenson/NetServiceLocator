using System;
using System.Collections.Generic;

namespace ServiceLocator.Discovery.Service
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
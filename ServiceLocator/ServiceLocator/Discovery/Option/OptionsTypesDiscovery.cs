using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ServiceLocator.Discovery.Option
{
	/// <summary>
	///		Loads all options that are set in its constructor
	/// </summary>
	public class OptionsTypesDiscovery : TypeBasedOptionDiscovery
	{
		private readonly IEnumerable<Type> _types;

		/// <summary>
		/// 
		/// </summary>
		public OptionsTypesDiscovery(IEnumerable<Type> types, IConfiguration configuration) : base(configuration)
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
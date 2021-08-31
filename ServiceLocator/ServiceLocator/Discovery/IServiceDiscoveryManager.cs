using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Defines a list of all <see cref="IServiceDiscovery"/> and allows to load types from it.
	/// </summary>
	public interface IServiceDiscoveryManager
	{
		/// <summary>
		///		The target <see cref="IServiceCollection"/> the <see cref="Locator.IServiceLocator"/> should import the Services into
		/// </summary>
		IServiceCollection ServiceCollection { get; }

		/// <summary>
		///		Returns a list of all <see cref="Type"/> that can be imported as a Service
		/// </summary>
		/// <returns></returns>
		IEnumerable<Type> DiscoverTypes();

		/// <summary>
		///		Adds a new <see cref="IServiceDiscovery"/> to its list
		/// </summary>
		/// <param name="discovery"></param>
		void AddDiscovery(IServiceDiscovery discovery);
	}
}
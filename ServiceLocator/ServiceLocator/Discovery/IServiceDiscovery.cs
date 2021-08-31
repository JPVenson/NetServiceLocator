using System;
using System.Collections.Generic;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Defines a way to discover services
	/// </summary>
	public interface IServiceDiscovery
	{
		/// <summary>
		///		Loads services that matches the given <see cref="filter"/>
		/// </summary>
		/// <param name="filter">A filter that checks for the presence of any of the <see cref="Attributes.ServiceAttribute"/></param>
		/// <returns>A list of types that should be evaluated by the <see cref="Locator.IServiceLocator"/></returns>
		IEnumerable<Type> DiscoverTypes(Func<Type, bool> filter);
	}
}
using Microsoft.Extensions.Configuration;

namespace ServiceLocator.Discovery.Option
{
	/// <summary>
	///		Extends the <see cref="IServiceDiscoveryManager"/> to include the required <see cref="IConfiguration"/>
	/// </summary>
	public interface IConfigurationDiscoveryManager : IServiceDiscoveryManager
	{
		IConfiguration Configuration { get; }
	}
}
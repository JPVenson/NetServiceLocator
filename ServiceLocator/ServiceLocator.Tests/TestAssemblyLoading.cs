using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using ServiceLocator.Discovery;
using ServiceLocator.Discovery.Option;
using ServiceLocator.Discovery.Service;

namespace ServiceLocator.Tests
{
	[TestFixture]
	public class TestAssemblyLoading
	{

		private Stream JsonfyText(string text)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(text));
		}

		public void TestPlayground()
		{
			IServiceCollection coll = null;
			coll.UseServiceDiscovery()
				.FromAppDomain()
				.DiscoverOptions(null)
				.FromAppDomain()
				.LocateServices();
		}

		[Test]
		public void TestCanLoadConfig()
		{
			var config = new ConfigurationBuilder();

			config.AddJsonStream(this.JsonfyText(@"{
	""TestSection"": {
		""ConfA"": ""Test"",
		""ConfB"": ""Data""
	}
}"));

			var configuration = config.Build();

			var section = configuration.GetSection("TestSection");
			Assert.That(section, Is.Not.Null);
			Assert.That(section["ConfA"], Is.Not.Null);
			Assert.That(section["ConfB"], Is.Not.Null);
			
			var services = new ServiceCollection()
				.UseServiceDiscovery()
				.DiscoverOptions(configuration)
				.FromTypes(new[] { typeof(FromConfigData) })
				.LocateServices();

			var serviceProvider = services.BuildServiceProvider();

			var configData = serviceProvider.GetRequiredService<IOptions<FromConfigData>>();
			Assert.That(configData, Is.Not.Null);
			Assert.That(configData.Value, Is.Not.Null);
			Assert.That(configData.Value.ConfA, Is.EqualTo("Test"));
			Assert.That(configData.Value.ConfB, Is.EqualTo("Data"));
		}
	}

	[FromConfig("TestSection")]
	public class FromConfigData
	{
		public string ConfA { get; set; }
		public string ConfB { get; set; }
	}
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Net.Http;

namespace QueuesTopics.Service.Test.SetupTest
{
	[TestFixture]
	public class BaseSetupTearDown
	{
		private TestServer _server;
		protected HttpClient _httpClient;

		[OneTimeSetUp]
		protected void InitSettings()
		{
			string settingsFileName = "appsettings.json";
			string settingsPath = Path.Combine(Directory.GetCurrentDirectory(), settingsFileName);

			_server = new TestServer(new WebHostBuilder()
										.UseStartup<Startup>()
										.UseConfiguration(new ConfigurationBuilder().AddJsonFile(settingsPath).Build()));
			_httpClient = _server.CreateClient();
		}

		[OneTimeTearDown]
		protected void ClearSettings()
		{
			_server.Dispose();
			_httpClient.Dispose();
		}
	}
}

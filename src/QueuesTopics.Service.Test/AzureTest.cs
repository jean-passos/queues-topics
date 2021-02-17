using QueuesTopics.Service.Test.SetupTest;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QueuesTopics.Service.Test
{
	[TestFixture]
	public class AzureTest : BaseSetupTearDown
	{
		[Test]
		public async Task SendPersonToAzureServiceBusQueue()
		{
			var body = new
			{
				name = "Joaquim Cauê Costa",
				birthDate = "27/09/1975",
				gender = "Masculino",
				fatherName = "Augusto João Eduardo Costa",
				motherName = "Maria Tânia",
				document = new
				{
					cpf = "25531278047",
					rg = "124744898"
				},
				contact = new
				{
					email = "joaquimcauecosta-71@oi15.com.br",
					phone = "9139336300",
					cellPhone = "91986249767"
				},
				address = new
				{
					zipCode = "66914090",
					streetName = "Alameda Moraes",
					number = 674,
					neighborhood = "Praia Grande (Mosqueiro)",
					city = "Belém",
					state = "PA"
				}
			};

			string payload = JsonConvert.SerializeObject(body);
			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/azure/queue", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}

		[Test]
		public async Task SendPersonToAzureServiceBusTopic()
		{
			var body = new
			{
				name = "Joaquim Cauê Costa",
				birthDate = "27/09/1975",
				gender = "Masculino",
				fatherName = "Augusto João Eduardo Costa",
				motherName = "Maria Tânia",
				document = new
				{
					cpf = "25531278047",
					rg = "124744898"

				},
				contact = new
				{
					email = "joaquimcauecosta-71@oi15.com.br",
					phone = "9139336300",
					cellPhone = "91986249767"

				},
				address = new
				{
					zipCode = "66914090",
					streetName = "Alameda Moraes",
					number = 674,
					neighborhood = "Praia Grande (Mosqueiro)",
					city = "Belém",
					state = "PA"
				}
			};

			string payload = JsonConvert.SerializeObject(body);
			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/azure/topic", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}
	}
}

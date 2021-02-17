using QueuesTopics.Service.Test.SetupTest;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QueuesTopics.Service.Test
{
	[TestFixture]
	public class RabbitMQTest : BaseSetupTearDown
	{

		[Test]
		public async Task SendPersonToQueueOnRabbitMQ()
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
			var response = await _httpClient.PostAsync("/api/rabbitmq/queue", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}

		[Test]
		public async Task SendPersonToTopicOnRabbitMQ()
		{
			var body = new
			{
				name = "Lucas Giovanni Rodrigues",
				birthDate = "27/05/1959",
				gender = "Masculino",
				fatherName = "Mário Francisco Rodrigues",
				motherName = "Julia Carolina Kamilly",
				document = new
				{
					cpf = "01926892585",
					rg = "411128139"
				},
				contact = new
				{
					email = "lucasgiovannirodrigues@malosti.com.br",
					phone = "5128143114",
					cellPhone = "51995392693"
				},
				address = new
				{
					zipCode = "91520657",
					streetName = "Beco Onze",
					number = 347,
					neighborhood = "São José",
					city = "Porto Alegre",
					state = "RS"
				}
			};

			string payload = JsonConvert.SerializeObject(body);
			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/rabbitmq/topic", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}
	}
}

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
		public async Task EnviaPessoaParaFila()
		{
			var body = new
			{
				nome = "Joaquim Cauê Costa",
				data_nasc = "27/09/1975",
				sexo = "Masculino",
				pai = "Augusto João Eduardo Costa",
				mae = "Maria Tânia",
				documento = new
				{
					cpf = "25531278047",
					rg = "124744898"
				},
				contato = new
				{
					email = "joaquimcauecosta-71@oi15.com.br",
					telefone_fixo = "9139336300",
					celular = "91986249767"
				},
				endereco = new
				{
					cep = "66914090",
					endereco = "Alameda Moraes",
					numero = 674,
					bairro = "Praia Grande (Mosqueiro)",
					cidade = "Belém",
					estado = "PA"
				}
			};

			string payload = JsonConvert.SerializeObject(body);
			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/rabbitmq/queue", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}

		[Test]
		public async Task EnviaPessoaParaTopico()
		{
			var body = new
			{
				nome = "Lucas Giovanni Rodrigues",
				data_nasc = "27/05/1959",
				sexo = "Masculino",
				pai = "Mário Francisco Rodrigues",
				mae = "Julia Carolina Kamilly",
				documento = new
				{
					cpf = "01926892585",
					rg = "411128139"
				},
				contato = new
				{
					email = "lucasgiovannirodrigues@malosti.com.br",
					telefone_fixo = "5128143114",
					celular = "51995392693"
				},
				endereco = new
				{
					cep = "91520657",
					endereco = "Beco Onze",
					numero = 347,
					bairro = "São José",
					cidade = "Porto Alegre",
					estado = "RS"
				}
			};

			string payload = JsonConvert.SerializeObject(body);
			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/rabbitmq/topic", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}
	}
}

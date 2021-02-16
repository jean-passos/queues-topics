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
			var response = await _httpClient.PostAsync("/api/azure/queue", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}

		[Test]
		public async Task EnviaPessoaParaTopico()
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
			var response = await _httpClient.PostAsync("/api/azure/topic", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}
	}
}

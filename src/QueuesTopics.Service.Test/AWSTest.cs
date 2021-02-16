using QueuesTopics.Service.Test.SetupTest;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QueuesTopics.Service.Test
{
	[TestFixture]
	public class AWSTest : BaseSetupTearDown
	{
		[Test]
		public async Task EnviaPessoaParaFila()
		{
			var body = new
			{
				nome = "Agatha Rebeca Aragão",
				data_nasc = "12/06/1965",
				sexo = "Feminino",
				pai = "Kauê Tiago Aragão",
				mae = "Louise Isabella Sophia",
				documento = new
				{
					cpf = "45823022387",
					rg = "297496086"
				},
				contato = new
				{
					email = "agatharebecaaragao@trilhavitoria.com.br",
					telefone_fixo = "7928321079",
					celular = "79992517053"
				},
				endereco = new
				{
					cep = "49075270",
					endereco = "Rua Frei Luiz Canolo de Noronha",
					numero = 362,
					bairro = "Siqueira Campos",
					cidade = "Aracaju",
					estado = "SE"
				}
			};

			string payload = JsonConvert.SerializeObject(body);

			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/aws/queue", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}

		[Test]
		public async Task EnviaPessoaParaTopico()
		{
			var body = new
			{
				nome = "Allana Valentina Pereira",
				data_nasc = "06/01/1998",
				sexo = "Feminino",
				mae = "Aline Louise",
				pai = "Osvaldo Lucca Geraldo Pereira",
				documento = new
				{
					cpf = "97349155417",
					rg = "313602499"
				},
				contato = new
				{
					email = "allanavalentina@yahooo.com.br",
					telefone_fixo = "8429492843",
					celular = "84993573974"
				},
				endereco = new
				{
					cep = "59035546",
					endereco = "Vila Presidente Mascarenhas",
					numero = 507,
					bairro = "Alecrim",
					cidade = "Natal",
					estado = "RN"
				}
			};

			string payload = JsonConvert.SerializeObject(body);

			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/aws/topic", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}
	}
}

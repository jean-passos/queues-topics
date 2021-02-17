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
		public async Task SendPersonToAWSSQS()
		{
			var body = new
			{
				name = "Agatha Rebeca Aragão",
				birthDate = "12/06/1965",
				gender = "Feminino",
				fatherName = "Kauê Tiago Aragão",
				motherName = "Louise Isabella Sophia",
				document = new
				{
					cpf = "45823022387",
					rg = "297496086"
				},
				contact = new
				{
					email = "agatharebecaaragao@trilhavitoria.com.br",
					phone = "7928321079",
					cellPhone = "79992517053"
				},
				address = new
				{
					zipCode = "49075270",
					address = "Rua Frei Luiz Canolo de Noronha",
					number = 362,
					neighborhood = "Siqueira Campos",
					city = "Aracaju",
					state = "SE"
				}
			};

			string payload = JsonConvert.SerializeObject(body);

			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/aws/queue", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}

		[Test]
		public async Task SendPersonToAWSSNS()
		{
			var body = new
			{
				name = "Allana Valentina Pereira",
				birthDate = "06/01/1998",
				gender = "Feminino",
				motherName = "Aline Louise",
				fatherName = "Osvaldo Lucca Geraldo Pereira",
				document = new
				{
					cpf = "97349155417",
					rg = "313602499"
				},
				contact = new
				{
					email = "allanavalentina@yahooo.com.br",
					phone = "8429492843",
					cellPhone = "84993573974"
				},
				address = new
				{
					zipCode = "59035546",
					address = "Vila Presidente Mascarenhas",
					number = 507,
					neighborhood = "Alecrim",
					city = "Natal",
					state = "RN"
				}
			};

			string payload = JsonConvert.SerializeObject(body);

			HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("/api/aws/topic", httpContent);

			Assert.DoesNotThrow(() => response.EnsureSuccessStatusCode());
		}
	}
}

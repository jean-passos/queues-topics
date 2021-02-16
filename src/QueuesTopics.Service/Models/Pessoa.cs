using Newtonsoft.Json;
using System;
using System.Globalization;

namespace QueuesTopics.Service.Models
{
	public class Pessoa
	{
		public string Nome { get; set; }
		[JsonProperty("data_nasc")]
		public string InDataNascimento { get; set; }

		public DateTime DataNascimento
		{
			get
			{
				DateTime.TryParseExact(InDataNascimento, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime dataNascimento);
				return dataNascimento;
			}
		}
		public string Sexo { get; set; }
		public string Pai { get; set; }
		public string Mae { get; set; }
		public Documento Documento { get; set; }
		public Contato Contato { get; set; }
		public Endereco Endereco { get; set; }
	}
}

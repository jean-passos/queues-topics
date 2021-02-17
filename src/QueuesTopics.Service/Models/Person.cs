using Newtonsoft.Json;
using System;
using System.Globalization;

namespace QueuesTopics.Service.Models
{
	public class Person
	{
		public string Name { get; set; }
		[JsonProperty("birthDate")]
		public string InBirthDate { get; set; }
		[JsonIgnore]
		public DateTime BirthDate
		{
			get
			{
				DateTime.TryParseExact(InBirthDate, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime birthDate);
				return birthDate;
			}
		}
		public string Gender { get; set; }
		public string FatherName { get; set; }
		public string MotherName { get; set; }
		public Document Document { get; set; }
		public Contact Contact { get; set; }
		public Address Address { get; set; }
	}
}

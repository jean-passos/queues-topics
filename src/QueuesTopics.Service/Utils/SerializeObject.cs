using Newtonsoft.Json;
using System.Text;

namespace QueuesTopics.Service.Utils
{
	public static class SerializeObject
	{

		public static string ConvertToJson(object toConvert)
		{
			return JsonConvert.SerializeObject(toConvert);
		}

		public static byte[] ConvertToByteArray(object toConvert)
		{
			return Encoding.UTF8.GetBytes(ConvertToJson(toConvert));
		}


	}
}

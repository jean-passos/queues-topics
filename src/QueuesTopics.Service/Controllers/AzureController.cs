using QueuesTopics.Service.Models;
using QueuesTopics.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace QueuesTopics.Service.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AzureController : ControllerBase
    {
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;
					
		public AzureController(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetSection("ServiceBusNamespace").GetSection("ConnectionString").Value;
		}

		[HttpPost("queue")]
		public async Task<IActionResult> SendToQueue([FromBody]Person person)
		{
			string queueName = _configuration.GetSection("ServiceBusNamespace").GetSection("QueueName").Value;

			try
			{
				IQueueClient queueClient = new QueueClient(_connectionString, queueName);
				Message message = new Message(SerializeObject.ConvertToByteArray(person));
				await queueClient.SendAsync(message);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpPost("topic")]
		public async Task<IActionResult> SendToTopic([FromBody]Person person)
		{
			string topicName = _configuration.GetSection("ServiceBusNamespace").GetSection("TopicName").Value;
			try
			{
				ITopicClient topicClient = new TopicClient(_connectionString, topicName);
				Message message = new Message(SerializeObject.ConvertToByteArray(person));
				message.CorrelationId = person.Address.State.ToUpper();
				await topicClient.SendAsync(message);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}
    }
}
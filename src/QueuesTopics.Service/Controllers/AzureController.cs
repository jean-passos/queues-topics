using QueuesTopics.Service.Models;
using QueuesTopics.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using QueuesTopics.Service.Settings;
using Microsoft.Extensions.Options;

namespace QueuesTopics.Service.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AzureController : ControllerBase
    {
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;
		private readonly ResourceNameSettings _resourceNameSettings;
					
		public AzureController(IConfiguration configuration, IOptions<ResourceNameSettings> resourceNameOptions)
		{
			_configuration = configuration;
			_resourceNameSettings = resourceNameOptions.Value;
			_connectionString = _configuration.GetConnectionString("ServiceBusConnectionString");
		}

		[HttpPost("queue")]
		public async Task<IActionResult> SendToQueue([FromBody]Person person)
		{
			try
			{
				IQueueClient queueClient = new QueueClient(_connectionString, _resourceNameSettings.Queue);
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
			try
			{
				ITopicClient topicClient = new TopicClient(_connectionString, _resourceNameSettings.Topic);
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
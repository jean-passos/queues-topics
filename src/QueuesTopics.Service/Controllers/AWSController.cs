using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Amazon.SQS.Model;
using QueuesTopics.Service.Models;
using QueuesTopics.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace QueuesTopics.Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AWSController : ControllerBase
	{
		private readonly IAmazonSQS _amazonSQS;
		private readonly IAmazonSimpleNotificationService _simpleNotificationService;
		private readonly IConfiguration _configuration;

		public AWSController(IAmazonSQS amazonSQS, IAmazonSimpleNotificationService simpleNotificationService, IConfiguration configuration)
		{
			_amazonSQS = amazonSQS;
			_simpleNotificationService = simpleNotificationService;
			_configuration = configuration;
		}

		[HttpPost("queue")]
		public async Task<IActionResult> SendPersonToQueue([FromBody] Person person)
		{
			try
			{
				SendMessageResponse messageResponse = await _amazonSQS.SendMessageAsync(BuildMessageToSend(person));
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpPost("topic")]
		public async Task<IActionResult> SendPersonToTopic([FromBody]Person person)
		{
			try
			{
				string topicArn = _configuration.GetSection("AWSSNSService").GetSection("TopicArn").Value;
				var response = await _simpleNotificationService.PublishAsync(topicArn, SerializeObject.ConvertToJson(person));

				return Ok();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex);
			}
		}

		private SendMessageRequest BuildMessageToSend(Person person)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			sendMessageRequest.MessageBody = SerializeObject.ConvertToJson(person);
			return sendMessageRequest;
		}
	}
}
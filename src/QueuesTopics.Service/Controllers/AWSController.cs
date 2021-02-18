using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Amazon.SQS.Model;
using QueuesTopics.Service.Models;
using QueuesTopics.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QueuesTopics.Service.Settings;

namespace QueuesTopics.Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AWSController : ControllerBase
	{
		private readonly IAmazonSQS _amazonSQS;
		private readonly IAmazonSimpleNotificationService _simpleNotificationService;
		private readonly IConfiguration _configuration;
		private readonly AWSSNSSettings _awsSNSSettings;

		public AWSController(IAmazonSQS amazonSQS, IAmazonSimpleNotificationService simpleNotificationService, IConfiguration configuration, IOptions<AWSSNSSettings> awsSNSOptions)
		{
			_amazonSQS = amazonSQS;
			_simpleNotificationService = simpleNotificationService;
			_configuration = configuration;
			_awsSNSSettings = awsSNSOptions.Value;
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
				var response = await _simpleNotificationService.PublishAsync(_awsSNSSettings.TopicArn, SerializeObject.ConvertToJson(person));
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
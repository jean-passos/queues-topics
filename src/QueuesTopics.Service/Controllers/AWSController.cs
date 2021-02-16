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
		public async Task<IActionResult> SendPersonToQueue([FromBody] Pessoa pessoa)
		{
			try
			{
				SendMessageResponse messageResponse = await _amazonSQS.SendMessageAsync(CriaMensagemParaEnvio(pessoa));
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpPost("topic")]
		public async Task<IActionResult> SendPersonToTopic([FromBody]Pessoa pessoa)
		{
			try
			{
				string topicArn = _configuration.GetSection("AWSSNSService").GetSection("TopicArn").Value;
				var response = await _simpleNotificationService.PublishAsync(topicArn, SerializeObject.ConvertToJson(pessoa));

				return Ok();
			}
			catch (Exception ex)
			{

				return StatusCode(500, ex);
			}
		}

		private SendMessageRequest CriaMensagemParaEnvio(Pessoa pessoa)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			sendMessageRequest.MessageBody = SerializeObject.ConvertToJson(pessoa);
			return sendMessageRequest;
		}
	}
}
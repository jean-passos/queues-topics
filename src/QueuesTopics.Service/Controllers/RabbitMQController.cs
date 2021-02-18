using QueuesTopics.Service.Models;
using QueuesTopics.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using QueuesTopics.Service.Settings;
using Microsoft.Extensions.Options;

namespace QueuesTopics.Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RabbitMQController : ControllerBase
	{
		private readonly IConnection _connection;
		private readonly ResourceNameSettings _resourceNameSettings;

		public RabbitMQController(IConnection connection, IOptions<ResourceNameSettings> resourceNameOptions)
		{
			_connection = connection;
			_resourceNameSettings = resourceNameOptions.Value;
		}

		[HttpPost("queue")]
		public IActionResult SendPersonToQueue([FromBody]Person person)
		{
			try
			{
				using (var model = _connection.CreateModel())
					model.BasicPublish("", _resourceNameSettings.Queue, null, SerializeObject.ConvertToByteArray(person));

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		[HttpPost("topic")]
		public IActionResult SendPersonToTopic([FromBody]Person person)
		{
			try
			{
				using(var channel = _connection.CreateModel())
					channel.BasicPublish(_resourceNameSettings.Topic, person.Address.State, null, SerializeObject.ConvertToByteArray(person));

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}
	}
}
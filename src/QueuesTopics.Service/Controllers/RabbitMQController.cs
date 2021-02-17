using QueuesTopics.Service.Models;
using QueuesTopics.Service.Utils;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;

namespace QueuesTopics.Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RabbitMQController : ControllerBase
	{
		private readonly IConnection _connection;

		public RabbitMQController(IConnection connection)
		{
			_connection = connection;
		}

		[HttpPost("queue")]
		public IActionResult SendPersonToQueue([FromBody]Person person)
		{
			string queueName = "fulano-q-pessoa";
			try
			{
				using (var model = _connection.CreateModel())
					model.BasicPublish("", queueName, null, SerializeObject.ConvertToByteArray(person));

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
			string exchangeName = "fulano-e-pessoa";
			try
			{
				using(var channel = _connection.CreateModel())
					channel.BasicPublish(exchangeName, person.Address.State, null, SerializeObject.ConvertToByteArray(person));

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}
	}
}
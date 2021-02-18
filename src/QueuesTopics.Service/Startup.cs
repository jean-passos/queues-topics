using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueuesTopics.Service.Settings;
using RabbitMQ.Client;

namespace QueuesTopics.Service
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			ConnectionFactory rabbitMQConnectionFactory = new ConnectionFactory();
			Configuration.Bind("RabbitMQ", rabbitMQConnectionFactory);
			services.AddSingleton(rabbitMQConnectionFactory.CreateConnection());

			services.Configure<ResourceNameSettings>(Configuration.GetSection("ResourceName"));
			services.Configure<AWSSNSSettings>(Configuration.GetSection("AWSSNS"));

			services.AddAWSService<IAmazonSQS>(Configuration.GetAWSOptions("AWSSQSService"));
			services.AddAWSService<IAmazonSimpleNotificationService>(Configuration.GetAWSOptions("AWSSNSService"));

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddJsonOptions(json => json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
				;
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}

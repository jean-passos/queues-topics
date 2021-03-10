# Queues and Topics
Usage of queues and topics on cloud providers Azure and AWS and local RabbitMQ instance in Asp.Net Core API

## Iac Folder

Files to create infrastructure on cloud providers Azure and AWS through deployment of ARM Templates and Cloud Formation respectively. This folder have two subfolders:

**aws-cloudformation**: template and parameters file to create on AWS: one queue (SQS) and one topic (SNS) with two queues, on AWS topics relates to many types of subscriptions as we want integrate two systems the choice was queues.

**azure-arm**: template and parameters file to create on Azure Service Bus: one queue and one topic with two subscriptions, each subscription have one filter so the messages that arrive will not send to all consumers, removing these filters all subscriptions receive a copy from every message.

## src Folder

**QueuesTopics.Service**: Project to send messages to message brokers

**QueuesTopics.Service.Test**: Project to test the integration between services and message brokers.

## Parameters Files and AppSettings
For resources on cloud providers both parameters file have fields to put queue and topic names, these names must to be the same present on appsettings.json file, section **ResourceName**, keys Queue and Topic. On RabbitMQ, the queue and topic have to be the same name as well, but it doesn't have a parameter file, their resources should be created by **RabbitMQ Management** or **CLI**.

- **aws_sqs_sns.parameters.json** file

```
[
	{
		"ParameterKey": "QueueName",
		"ParameterValue": "pessoa-queue"
	},
	{
		"ParameterKey": "TopicName",
		"ParameterValue": "pessoa-topic"
	}
]
```

- **servicebusdeploy.parameters.json** file

```
...
  "queue_name": {
      "value": "pessoa-queue"
  },
  "topic_name": {
      "value": "pessoa-topic"
  }
...
```

- **appsettings.json** file
```
"ResourceName": {
	"Queue": "pessoa-queue",
	"Topic": "pessoa-topic"
}
```

After deploy the files to ARM template on Azure and CloudFormation on AWS, some output parameters should be get to fill values on appsettings file. However, for RabbitMQ must be filled the hostname and credentials.

### ARM Template - Azure

Executing via PowerShell get the **NamespaceConnectionString** output value and put on the blank value for ServiceBusConnectionString key
```
...
"ConnectionStrings": {
	"ServiceBusConnectionString": ""
}
...
```

### CloudFormation - AWS

AWS CloudFormation has two ouputs one for Queues and another for Topics. Go to CloudFormation console, access the stack created and **Outputs** tab.  
Get the value from key **QueueURL** and set value for **ServiceURL** key on section **AWSSQSService**.
```
"AWSSQSService": {
	"Profile": "sqs_profile",
	"ServiceURL": ""
}
```
For topics, pick up **PessoaTopicArn** value and set value for **TopicArn** key on section **AWSSNS**.
```
"AWSSNS": {
	"TopicArn": ""
}
```

### RabbitMQ
For RabbitMQ the **RabbitMQ** section has to filled with the hostname and access credentials, once the queue and topic were created earlier and they respect the ResourceName section subkey values
```
"RabbitMQ": {
	"HostName": "",
	"UserName": "",
	"Password": ""
}
```
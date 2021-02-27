# Queues and Topics
Usage of queues and topics on cloud providers Azure and AWS and local RabbitMQ instance in Asp.Net Core API

## Iac Folder

Files to create infrastructure on cloud providers Azure and AWS through deployment of ARM Templates and Cloud Formation respectively. This folder have two subfolders:

**aws-cloudformation**: template and parameters file to create on AWS: one queue (SQS) and one topic (SNS) with two queues, on AWS topics relates to many types of subscriptions as we want integrate two systems the choice was queues.

**azure-arm**: template and parameters file to create on Azure Service Bus: one queue and one topic with two subscriptions, each subscription have one filter so the messages that arrive will not send to all consumers, if remove these filters all subscriptions receive a copy from each message.

## src Folder

**QueuesTopics.Service**: Project to send messages to message brokers

**QueuesTopics.Service.Test**: Project to test the integration between services and message brokers.
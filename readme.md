# Azure Kafka Trigger Function Examples

To set up a Kafka cluster and producer see [Confluent Kafka Producer Examples](https://github.com/VinnyBonner/ConfluentKafkaProducerExample)

Examples are of [Kafka Trigger Function](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-kafka-trigger?tabs=in-process%2Cconfluent&pivots=programming-language-csharp) in with [CosmosDB Out](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-output?tabs=in-process%2Cfunctionsv2&pivots=programming-language-csharp)

Examples show Kafka [Single Mode](https://github.com/VinnyBonner/AzureKafkaTriggerFunctionExamples/tree/main/SingleMode), [Batch Mode](https://github.com/VinnyBonner/AzureKafkaTriggerFunctionExamples/tree/main/BatchMode), [Concurrency](https://github.com/VinnyBonner/AzureKafkaTriggerFunctionExamples/tree/main/ConcurrencyMode) and [Avro Schema] (https://github.com/VinnyBonner/AzureKafkaTriggerFunctionExamples/tree/main/Avro)

A semi-outdated doc but it has some great information into the architecture of the Kafka Trigger [Azure Functions Kafka Trigger Performance Tips, Concept, and Architecture](https://tsuyoshiushio.medium.com/azure-functions-kafka-trigger-performance-tips-concept-and-architecture-ec94a31d8b93)

Azure uses Librdkafka to connect to kafka and there are a couple of the settings that can be configured via host.json

```
Property                  Applies to      librdkafka equivalent
----------------------------------------------------------------------
AutoCommitIntervalMs      Trigger         auto.commit.interval.ms
FetchMaxBytes             Trigger         fetch.max.bytes
LibkafkaDebug             Both            debug
MaxPartitionFetchBytes    Trigger         max.partition.fetch.bytes
MaxPollIntervalMs         Trigger         max.poll.interval.ms
MetadataMaxAgeMs          Both            metadata.max.age.ms
QueuedMinMessages         Trigger         queued.min.messages
QueuedMaxMessagesKbytes   Trigger         queued.max.messages.kbytes
ReconnectBackoffMs        Trigger         reconnect.backoff.max.ms
ReconnectBackoffMaxMs     Trigger         reconnect.backoff.max.ms
SessionTimeoutMs          Trigger         session.timeout.ms
SocketKeepaliveEnable     Both            socket.keepalive.enable
StatisticsIntervalMs      Trigger         statistics.interval.ms
```

You can review the defaults and descriptions of the librdkafka settings here [Librdkafka Global Configuration Properties](https://docs.confluent.io/platform/current/clients/librdkafka/html/md_CONFIGURATION.html)

### [Azure Functions Kafka Extension Sample Code in Multiple Languages](https://github.com/Azure/azure-functions-kafka-extension/tree/dev/samples)

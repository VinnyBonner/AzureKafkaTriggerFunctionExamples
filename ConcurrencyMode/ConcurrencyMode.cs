using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using KafkaTrigger.CosmosClass;

namespace KafkaTrigger.ConcurrencyMode
{
    public class ConcurrencyOne
    {

        [FunctionName("ConcurrencyOne")]
        public static async Task Run(
            [KafkaTrigger("BrokerList",
                          "MultipleConsumersMode",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            [CosmosDB (
                databaseName: "KafkaMessages",
                collectionName: "kafkaContainer",
                ConnectionStringSetting  = "CosmosDBCon"
            )] IAsyncCollector<KafkaMessage> document,
            ExecutionContext executionContext,
            ILogger log)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();
                var invocationId = executionContext.InvocationId;

                foreach (KafkaEventData<string> eventData in events)
                {
                    var message = new KafkaMessage
                    {
                        BatchGUID = batchGuid,
                        ExecutionTime = now,
                        BatchSize = events.Length,
                        InvocationID = invocationId.ToString(),
                        MessageGUID = Guid.NewGuid().ToString(),
                        Message = eventData.Value,
                        Partition = eventData.Partition,
                        KafkaTimeStamp = eventData.Timestamp,
                        MessageOffset = eventData.Offset.ToString(),
                        TriggeredFunction = "Concurrency 1"
                    };

                    await document.AddAsync(message);

                    message.log(log);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "ConcurrencyMode", ex.InnerException);
            }
        }
    }

    public class ConcurrencyTwo
    {

        [FunctionName("ConcurrencyTwo")]
        public static async Task Run(
            [KafkaTrigger("BrokerList",
                          "MultipleConsumersMode",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            [CosmosDB (
                databaseName: "KafkaMessages",
                collectionName: "kafkaContainer",
                ConnectionStringSetting  = "CosmosDBCon"
            )] IAsyncCollector<KafkaMessage> document,
            ExecutionContext executionContext,
            ILogger log)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();
                var invocationId = executionContext.InvocationId;

                foreach (KafkaEventData<string> eventData in events)
                {
                    var message = new KafkaMessage
                    {
                        BatchGUID = batchGuid,
                        ExecutionTime = now,
                        BatchSize = events.Length,
                        InvocationID = invocationId.ToString(),
                        MessageGUID = Guid.NewGuid().ToString(),
                        Message = eventData.Value,
                        Partition = eventData.Partition,
                        KafkaTimeStamp = eventData.Timestamp,
                        MessageOffset = eventData.Offset.ToString(),
                        TriggeredFunction = "Concurrency 2"
                    };

                    await document.AddAsync(message);

                    message.log(log);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "ConcurrencyMode", ex.InnerException);
            }
        }
    }

    public class ConcurrencyThree
    {

        [FunctionName("ConcurrencyThree")]
        public static async Task Run(
            [KafkaTrigger("BrokerList",
                          "MultipleConsumersMode",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            [CosmosDB (
                databaseName: "KafkaMessages",
                collectionName: "kafkaContainer",
                ConnectionStringSetting  = "CosmosDBCon"
            )] IAsyncCollector<KafkaMessage> document,
            ExecutionContext executionContext,
            ILogger log)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();
                var invocationId = executionContext.InvocationId;

                foreach (KafkaEventData<string> eventData in events)
                {
                    var message = new KafkaMessage
                    {
                        BatchGUID = batchGuid,
                        ExecutionTime = now,
                        BatchSize = events.Length,
                        InvocationID = invocationId.ToString(),
                        MessageGUID = Guid.NewGuid().ToString(),
                        Message = eventData.Value,
                        Partition = eventData.Partition,
                        KafkaTimeStamp = eventData.Timestamp,
                        MessageOffset = eventData.Offset.ToString(),
                        TriggeredFunction = "Concurrency 3"
                    };

                    await document.AddAsync(message);

                    message.log(log);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "ConcurrencyMode", ex.InnerException);
            }
        }
    }

    public class ConcurrencyFour
    {

        [FunctionName("ConcurrencyFour")]
        public static async Task Run(
            [KafkaTrigger("BrokerList",
                          "MultipleConsumersMode",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            [CosmosDB (
                databaseName: "KafkaMessages",
                collectionName: "kafkaContainer",
                ConnectionStringSetting  = "CosmosDBCon"
            )] IAsyncCollector<KafkaMessage> document,
            ExecutionContext executionContext,
            ILogger log)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();
                var invocationId = executionContext.InvocationId;

                foreach (KafkaEventData<string> eventData in events)
                {
                    var message = new KafkaMessage
                    {
                        BatchGUID = batchGuid,
                        ExecutionTime = now,
                        BatchSize = events.Length,
                        InvocationID = invocationId.ToString(),
                        MessageGUID = Guid.NewGuid().ToString(),
                        Message = eventData.Value,
                        Partition = eventData.Partition,
                        KafkaTimeStamp = eventData.Timestamp,
                        MessageOffset = eventData.Offset.ToString(),
                        TriggeredFunction = "Concurrency 4"
                    };

                    await document.AddAsync(message);

                    message.log(log);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "ConcurrencyMode", ex.InnerException);
            }
        }
    }

    public class ConcurrencyFive
    {

        [FunctionName("ConcurrencyFive")]
        public static async Task Run(
            [KafkaTrigger("BrokerList",
                          "MultipleConsumersMode",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            [CosmosDB (
                databaseName: "KafkaMessages",
                collectionName: "kafkaContainer",
                ConnectionStringSetting  = "CosmosDBCon"
            )] IAsyncCollector<KafkaMessage> document,
            ExecutionContext executionContext,
            ILogger log)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();
                var invocationId = executionContext.InvocationId;

                foreach (KafkaEventData<string> eventData in events)
                {
                    var message = new KafkaMessage
                    {
                        BatchGUID = batchGuid,
                        ExecutionTime = now,
                        BatchSize = events.Length,
                        InvocationID = invocationId.ToString(),
                        MessageGUID = Guid.NewGuid().ToString(),
                        Message = eventData.Value,
                        Partition = eventData.Partition,
                        KafkaTimeStamp = eventData.Timestamp,
                        MessageOffset = eventData.Offset.ToString(),
                        TriggeredFunction = "Concurrency 5"
                    };

                    await document.AddAsync(message);

                    message.log(log);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "ConcurrencyMode", ex.InnerException);
            }
        }
    }
}

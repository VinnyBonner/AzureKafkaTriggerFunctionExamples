using Avro.Generic;
using Avro.Specific;
using KafkaTrigger.CosmosClass;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KafkaTrigger.Avro
{
    public class AvroExample
    {

        [FunctionName("AvroExample")]
        public static void Run(
            [KafkaTrigger("BrokerList",
                          "AvroTopic",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string, MySchema> kafkaEvent,
            [CosmosDB (
                databaseName: "KafkaMessages",
                collectionName: "kafkaContainer",
                ConnectionStringSetting  = "CosmosDBCon"
            )]out KafkaMessage document,
            ExecutionContext executionContext,
            ILogger log)
        {
            try
            {
                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();

                document = new KafkaMessage
                {
                    ExecutionTime = now,
                    InvocationID = executionContext.InvocationId.ToString(),
                    MessageGUID = Guid.NewGuid().ToString(),
                    Message = kafkaEvent.Value.GetAll(),
                    Partition = kafkaEvent.Partition,
                    KafkaTimeStamp = kafkaEvent.Timestamp,
                    MessageOffset = kafkaEvent.Offset.ToString(),
                    TriggeredFunction = "AvroExample"
                };

                document.Log(log);

            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.Empty;

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "AvroExample", ex.InnerException);
            }
        }
    }
}

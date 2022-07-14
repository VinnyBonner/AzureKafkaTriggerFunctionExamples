using KafkaTrigger.CosmosClass;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KafkaTrigger.SingleMode
{
    public class SingleMode
    {

        [FunctionName("SingleMode")]
        public static void Run(
            [KafkaTrigger("BrokerList",
                          "SingleMode",
                          Username = "KafkaUser",
                          Password = "KafkaPassword",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string> kafkaEvent,
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
                    Message = kafkaEvent.Value,
                    Partition = kafkaEvent.Partition,
                    KafkaTimeStamp = kafkaEvent.Timestamp,
                    MessageOffset = kafkaEvent.Offset.ToString(),
                    TriggeredFunction = "SingleMode"
                };                

                document.Log(log);

            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "SingleMode", ex.InnerException);
            }
        }
    }
}

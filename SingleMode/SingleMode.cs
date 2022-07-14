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
        public static async Task Run(
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
            )] KafkaMessage document,
            ExecutionContext executionContext,
            ILogger log)
        {

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                var now = DateTime.UtcNow;
                var batchGuid = Guid.NewGuid().ToString();

                document.ExecutionTime = now;
                document.InvocationID = executionContext.InvocationId.ToString();
                document.MessageGUID = Guid.NewGuid().ToString();
                document.Message = kafkaEvent.Value;
                document.Partition = kafkaEvent.Partition;
                document.KafkaTimeStamp = kafkaEvent.Timestamp;
                document.MessageOffset = kafkaEvent.Offset.ToString();
                document.TriggeredFunction = "SingleMode";

                document.log(log);

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

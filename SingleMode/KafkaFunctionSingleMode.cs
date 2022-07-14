using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.DataLake.Store;
using System.IO;
using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Azure.Management.DataLake.Store;

namespace KafkaTrigger
{
    public class SingleMode
    {
        // Kafka Message in CosmosDB Out
        [FunctionName("SingleMode")]
        public async Task Run(
            [KafkaTrigger("BrokerList",
                          "SingleMode",
                          Username = "KafkaUser", // AppSettings with UserName - Confluent Key
                          Password = "KafkaPassword", // AppSetting with Kafka Password - Confluent Secret
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
                log.LogError($"ERROR: {ex.ToString()}");

                if (!Guid.TryParse(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"), out Guid instanceId))
                    instanceId = Guid.NewGuid();

                throw new Microsoft.Azure.WebJobs.Host.FunctionInvocationException(ex.Message, instanceId, "SingleMode", ex.InnerException);
            }
        }
    }

    //CostmosDB Class
    public class KafkaMessage
    {
        private readonly string _instanceId;
        public KafkaMessage() {
            _instanceId = System.Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");
        }
                
        public DateTime ExecutionTime { get; set; }

        public int BatchSize { get; set; }
        public string BatchGUID { get; set; }
        public string InvocationID { get; set; }
        public string Message { get; set; }
        public string MessageGUID { get; set; }
        public string InstanceID { get { return _instanceId; } }
        public int Partition { get; set; }
        public DateTime KafkaTimeStamp { get; set; }
        public string MessageOffset { get; set; }
        public string TriggeredFunction { get; set; }

        public void log(ILogger log)
        {
            // Log first 4 of instanceId, Partition, FunctionTriggered and the Message
            log.LogInformation($"InstanceID {InstanceID.Substring(0,4)} Partition {Partition} Func {TriggeredFunction} Message {Message}");
        }
    }
}

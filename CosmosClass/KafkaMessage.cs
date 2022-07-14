using Microsoft.Extensions.Logging;
using System;

namespace KafkaTrigger.CosmosClass
{
    public class KafkaMessage
    {
        private readonly string _instanceId;
        public KafkaMessage()
        {
            _instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");
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
            log.LogInformation($"InstanceID {InstanceID.Substring(0, 3)} Partition {Partition} Func {TriggeredFunction} Message {Message}");
        }
    }
}

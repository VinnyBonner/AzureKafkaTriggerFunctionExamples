using Avro;
using Avro.Specific;
using Newtonsoft.Json;

namespace KafkaTrigger.Avro
{
    public class MySchema : ISpecificRecord
    {
        public const string SchemaText = @"
       {
          ""type"": ""record"",
          ""name"": ""MySchema"",
          ""namespace"": ""KafkaTrigger.Avro"",
          ""fields"": [
            {
              ""name"": ""id"",
              ""type"": ""string""
            },
            {
              ""name"": ""amount"",
              ""type"": ""double""
            },
            {
              ""name"": ""type"",
              ""type"": ""string""
            },
            {
              ""name"": ""payoutId"",
              ""type"": ""string""
            },
            {
              ""name"": ""payoutStatus"",
              ""type"": ""string""
            },
            {
              ""name"": ""currency"",
              ""type"": ""string""
            },
            {
              ""name"": ""fee"",
              ""type"": ""double""
            },
            {
              ""name"": ""sourceId"",
              ""type"": ""string""
            },
            {
              ""name"": ""sourceType"",
              ""type"": ""string""
            },
            {
              ""name"": ""processedAt"",
              ""type"": ""string""
            },
            {
              ""name"": ""customerId"",
              ""type"": ""string""
            },
            {
              ""name"": ""walletId"",
              ""type"": ""string""
            }
          ]
        }";

        public static Schema _SCHEMA = Schema.Parse(SchemaText);

        [JsonIgnore]
        public virtual Schema Schema => _SCHEMA;
        public string Id { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public string PayoutId { get; set; }
        public string PayoutStatus { get; set; }
        public string Currency { get; set; }
        public double Fee { get; set; }
        public string SourceId { get; set; }
        public string SourceType { get; set; }
        public string ProcessedAt { get; set; }
        public string CustomerId { get; set; }
        public string WalletId { get; set; }

        public virtual object Get(int fieldPos)
        {
            switch (fieldPos)
            {
                case 0: return this.Id;
                case 1: return this.Amount;
                case 2: return this.Type;
                case 3: return this.PayoutId;
                case 4: return this.PayoutStatus;
                case 5: return this.Currency;
                case 6: return this.Fee;
                case 7: return this.SourceId;
                case 8: return this.SourceType;
                case 9: return this.ProcessedAt;
                case 10: return this.CustomerId;
                case 11: return this.WalletId;
                default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
            };
        }
        public virtual void Put(int fieldPos, object fieldValue)
        {
            switch (fieldPos)
            {
                case 0: this.Id = (string)fieldValue; break;
                case 1: this.Amount = (double)fieldValue; break;
                case 2: this.Type = (string)fieldValue; break;
                case 3: this.PayoutId = (string)fieldValue; break;
                case 4: this.PayoutStatus = (string)fieldValue; break;
                case 5: this.Currency = (string)fieldValue; break;
                case 6: this.Fee = (double)fieldValue; break;
                case 7: this.SourceId = (string)fieldValue; break;
                case 8: this.SourceType = (string)fieldValue; break;
                case 9: this.ProcessedAt = (string)fieldValue; break;
                case 10: this.CustomerId = (string)fieldValue; break;
                case 11: this.WalletId = (string)fieldValue; break;
                default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
            };
        }

        public string GetAll()
        {
            return $"ID: {this.Id}, Amount: {this.Amount}, Type: {this.Type}, PayoutId: {this.PayoutId}, PayoutStatus: {this.PayoutStatus}, " +
                $"Currency: {this.Currency}, Fee: {this.Fee}, SourceId: {this.SourceId}, SourceType: {this.SourceType}, " +
                $"ProcessedAt: {this.ProcessedAt}, CustomerId: {this.CustomerId}, WalletId: {this.WalletId}";
        }
    }
}

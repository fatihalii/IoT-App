using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HefaWebhook.Models
{
    public class Signal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public double Co { get; set; }
        public double Co2 { get; set; }
        public double So2 { get; set; } 
        public double No2 { get; set; }
        public double O3{ get; set; }
        public double Ch4 { get; set; }
        public double Pm25 { get; set; }
        public double Pm10 { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}

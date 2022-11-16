namespace HefaWebhook.Models
{
    public class RawSignal
    {
        public UplinkMessage? uplink_message { get; set; }

        public class UplinkMessage
        {
            public DecodedPayload? decoded_payload { get; set; }
        }

    }


}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXC.Utils.CorrectTimestamps
{
    public class DoublyTimestampedDataPayload : ITimestampedData
    {
        public DoublyTimestampedDataPayload(string payload, DateTimeOffset timestamp, DateTimeOffset referenceTimestamp)
        {
            Payload = payload;
            Timestamp = timestamp;
            ReferenceTimestamp = referenceTimestamp;
        }

        public string Payload { get; }

        public DateTimeOffset Timestamp { get; }

        public DateTimeOffset ReferenceTimestamp { get; }
    }
}

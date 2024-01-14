using ProtoBuf;

namespace DevTool
{
    [ProtoContract()]
    public class QueueData
    {
        [ProtoMember(1, IsRequired = true)]
        public UInt64 timestamp { get; set; }
        [ProtoMember(2, IsRequired = true)]
        public double temperature { get; set; }
        [ProtoMember(3, IsRequired = true)]
        public double humidity { get; set; }
        [ProtoMember(4, IsRequired = false)]
        public string? identifier { get; set; }
    }

}

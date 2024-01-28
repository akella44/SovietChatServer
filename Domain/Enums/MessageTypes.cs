using System.Runtime.Serialization;

namespace Domain.Enums;

public enum MessageTypes
{
    [EnumMember(Value = "Audio")]
    Audio,
    [EnumMember(Value = "Video")]
    Video,
    [EnumMember(Value = "Message")]
    Message,
    [EnumMember(Value = "Voice")]
    VoiceMessage
}
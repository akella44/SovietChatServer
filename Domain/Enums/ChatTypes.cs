using System.Runtime.Serialization;

namespace Domain.Enums;

public enum ChatTypes
{
    [EnumMember(Value = "Group")]
    Group,
    [EnumMember(Value = "Private")]
    Private
}
using System.Runtime.Serialization;

namespace Domain.Enums {
    public enum StatusNameEnum {

        [EnumMember(Value = "Ordered")]
        ordered,
        [EnumMember(Value = "Pending")]
        pending,
        [EnumMember(Value = "Ready")]
        ready,
        [EnumMember(Value = "Delivered")]
        delivered,
        [EnumMember(Value = "Paid")]
        paid
    }
}
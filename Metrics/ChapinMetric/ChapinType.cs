using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;


public enum ChapinType
{
	[EnumMember(Value = "P")]
	P,
	[EnumMember(Value = "M")]
	M,
	[EnumMember(Value = "C")]
	C,
	[EnumMember(Value = "T")]
	T
}
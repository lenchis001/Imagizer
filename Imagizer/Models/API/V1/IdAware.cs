using System.Runtime.Serialization;

namespace Imagizer.Models.API.V1
{
	[DataContract]
	public class IdAware
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }
	}
}

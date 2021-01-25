using System.Runtime.Serialization;

namespace Imagizer.Models.API.V1.Images
{
	[DataContract]
	public class Image : IdAware
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "userId")]
		public int UserId { get; set; }
	}
}

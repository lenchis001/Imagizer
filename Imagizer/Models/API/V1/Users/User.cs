using System.Runtime.Serialization;

namespace Imagizer.Models.API.V1.Users
{
	[DataContract]
	public class User : IdAware
	{
		[DataMember(Name = "email")]
		public string Email { get; set; }

		[DataMember(Name = "apiKey")]
		public string ApiKey { get; set; }

		[DataMember(Name = "password")]
		public string Password { get; set; }
	}
}

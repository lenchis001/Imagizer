using System.Collections.Generic;

namespace Imagizer.DAL.Models
{
	public class User : IdAware
	{
		public string PasswordHash { get; set; }
		public string Email { get; set; }
		public string ApiKey { get; set; }
		public ICollection<Image> Images { get; set; }
	}
}

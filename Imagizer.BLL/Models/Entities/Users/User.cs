namespace Imagizer.BLL.Models.Entities.Users
{
	public class User : IdAware
	{
		public string PasswordHash { get; set; }
		public string Email { get; set; }
		public string ApiKey { get; set; }
	}
}

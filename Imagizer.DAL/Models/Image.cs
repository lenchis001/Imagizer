namespace Imagizer.DAL.Models
{
	public class Image : IdAware
	{
		public string Name { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
	}
}

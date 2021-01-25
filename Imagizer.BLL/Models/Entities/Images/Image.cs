namespace Imagizer.BLL.Models.Entities.Images
{
	public class Image : IdAware
	{
		public string Name { get; set; }
		public int UserId { get; set; }
	}
}

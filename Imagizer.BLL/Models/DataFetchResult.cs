namespace Imagizer.BLL.Models
{
	public class DataFetchResult<D, E>
	{
		public E Error { get; set; }

		public D Data { get; set; }
	}
}

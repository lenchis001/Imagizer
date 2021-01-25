namespace Imagizer.Models.API.V1
{
	public class DataFetchResult<D, E>
	{
		public E Error { get; set; }

		public D Data { get; set; }
	}
}

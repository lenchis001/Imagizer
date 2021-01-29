namespace Imagizer.BLL.Services
{
	public interface IFileMetadataService
	{
		string GetExtentionByContentType(string contentType);

		string GetContentTypeByExtention(string extention);
	}
}

namespace Imagizer.BLL.Services
{
	class FileMetadataService : IFileMetadataService
	{
		private const string MIME_TYPE_PREFIX = "image/";

		public string GetContentTypeByExtention(string extention)
		{
			return MIME_TYPE_PREFIX + extention.Substring(1);
		}

		public string GetExtentionByContentType(string contentType)
		{
			return contentType.StartsWith(MIME_TYPE_PREFIX) ? $".{contentType.Replace(MIME_TYPE_PREFIX, string.Empty)}" : null;
		}
	}
}

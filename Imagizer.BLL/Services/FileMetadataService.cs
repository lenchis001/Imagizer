namespace Imagizer.BLL.Services
{
	class FileMetadataService : IFileMetadataService
	{
		private const string IMAGE_JPEG_CONTENT_TYPE = "image/jpeg";
		private const string JPEG_EXTENTION = ".jpeg";

		public string GetContentTypeByExtention(string extention)
		{
			switch (extention)
			{
				case JPEG_EXTENTION:
					return IMAGE_JPEG_CONTENT_TYPE;
				default:
					return null;
			}
		}

		public string GetExtentionByContentType(string contentType)
		{
			switch (contentType)
			{
				case IMAGE_JPEG_CONTENT_TYPE:
					return JPEG_EXTENTION;
				default:
					return null;
			}
		}
	}
}

using Imagizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	class ImageRepository : IImageRepository
	{
		private readonly string _storagePath;

		public ImageRepository(string storagePath = null)
		{
			_storagePath = storagePath ?? Environment.GetEnvironmentVariable(Resources.STORAGE_PATH_ENVIRONMENT_VARIABLE_NAME);
		}

		public async Task<Image> AddAsync(AddImageData image)
		{
			try
			{
				var name = Guid.NewGuid().ToString();
				var newFilePath = Path.Combine(_storagePath, name);

				await File.WriteAllBytesAsync(newFilePath, Convert.FromBase64String(image.Data));

				return new Image
				{
					Name = name
				};
			}
			catch
			{
				return null;
			}
		}

		public Task<DeleteErrorCode> DeleteAsync(Image image)
		{
			//var filePath = Path.Combine(_storagePath, image.Name);
			//File.Delete();
			throw new NotImplementedException();
		}

		public Task<ICollection<Image>> GetAllAsync()
		{
			throw new NotImplementedException();
		}
	}
}

using Imagizer.DAL.Models;
using System;
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
				var name = Guid.NewGuid().ToString() + image.Extention;
				var newFilePath = Path.Combine(_storagePath, name);

				await File.WriteAllBytesAsync(newFilePath, image.Data);

				return new Image
				{
					Name = name,
					Data = image.Data
				};
			}
			catch
			{
				return null;
			}
		}

		public Task<DeleteErrorCode> DeleteAsync(string name)
		{
			var result = DeleteErrorCode.OK;

			try
			{
				var filePath = Path.Combine(_storagePath, name);

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
				else
				{
					result = DeleteErrorCode.NOT_FOUND;
				}
			}
			catch (DirectoryNotFoundException)
			{
				result = DeleteErrorCode.NOT_FOUND;
			}
			catch (Exception)
			{
				result = DeleteErrorCode.UNKNOWN;
			}

			return Task.FromResult(result);
		}

		public async Task<Image> GetAsync(string name)
		{
			var result = new Image
			{
				Name = name
			};

			try
			{
				var filePath = Path.Combine(_storagePath, name);

				var data = await File.ReadAllBytesAsync(filePath);

				result.Data = data;
			}
			catch (Exception)
			{
				result.Data = new byte[0];
			}

			return result;
		}
	}
}

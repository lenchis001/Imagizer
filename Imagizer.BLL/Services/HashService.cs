using System;
using System.IO;
using System.Security.Cryptography;

namespace Imagizer.BLL.Services
{
	public class HashService : IHashService
	{
		public string MakeHash(string value)
		{
			// Create a new AesManaged.    
			using (AesManaged aes = new AesManaged())
			{
				// Create encryptor    
				ICryptoTransform encryptor = aes.CreateEncryptor(
					Convert.FromBase64String(Resources.ENCRYPTION_KEY),
					Convert.FromBase64String(Resources.IV)
					);
				// Create MemoryStream    
				using (MemoryStream ms = new MemoryStream())
				{
					// Create crypto stream using the CryptoStream class. This class is the key to encryption    
					// and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
					// to encrypt    
					using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					{
						// Create StreamWriter and write data to a stream    
						using (StreamWriter sw = new StreamWriter(cs))
							sw.Write(value);
						return Convert.ToBase64String(ms.ToArray());
					}
				}
			}
		}
	}
}

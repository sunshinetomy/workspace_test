using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_MXL
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string txtName = "Test_do.txt";
				string txtPath = "F:\\workspace_suny\\workspace_test\\Test_MXL\\Test_MXL\\Resources\\";
				//string decompressName = "new.gz";
				string decompressName = "Dichterliebe01.mxl";
				//string decompressPath = "F:\\workspace_suny\\workspace_test\\Test_MXL\\Test_MXL\\Resources\\";
				string decompressPath = "D:\\workspace_test\\Test_MXL\\Test_MXL\\Resources\\";
				//string zipName = "testZipFile.zip";
				//string zipName = "Dichterliebe01.mxl";
				string zipName = "89280-Twinkle_Twinkle_Little_Star.mxl";

				//string fullPath = "F:\\workspace_suny\\TestProject\\Test_MXL\\Test_MXL\\Resources\\Test_do.txt";
				//string path = Path.Combine(Environment.CurrentDirectory, @"Resources\", "Test_do.txt");

				// 1.
				// Starting file is 26,747 bytes.
				//string anyString = File.ReadAllText(txtPath + txtName);
				//Console.WriteLine("anyString: {0}", anyString);

				// 2.
				// Output file is 7,388 bytes.
				//CompressStringToFile(decompressPath + decompressName, anyString);

				//string decompressName = "89280-Twinkle_Twinkle_Little_Star.mxl";
				
				//DecompressStringFromFile( decompressPath + decompressName );

				DecompressToPath( decompressPath + "zipFolder", decompressPath + zipName, decompressPath + "extractFolder" );

				//loadXML(decompressPath + decompressName);
			}
			catch( Exception _exception )
			{
				Console.WriteLine("Exception: {0}", _exception);
				// Could not compress.
			}

			Console.ReadLine();
		}

		public static void CompressStringToFile(string fileName, string value)
		{
			// A.
			// Write string to temporary file.
			string temp = Path.GetTempFileName();
			File.WriteAllText(temp, value);

			// B.
			// Read file into byte array buffer.
			byte[] b;
			using (FileStream f = new FileStream(temp, FileMode.Open))
			{
				b = new byte[f.Length];
				f.Read(b, 0, (int)f.Length);
			}

			// C.
			// Use GZipStream to write compressed bytes to target file.
			using (FileStream f2 = new FileStream(fileName, FileMode.Create))
			using (GZipStream gz = new GZipStream(f2, CompressionMode.Compress, false))
			{
				gz.Write(b, 0, b.Length);
			}
		}

		private static void DecompressStringFromFile( string _fullPath )
		{
			FileInfo fileToDecompress = new FileInfo(_fullPath);
			
			using (FileStream originalFileStream = fileToDecompress.OpenRead())
			{
				//string currentFileName = fileToDecompress.FullName;
				//string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
				//
				//using (FileStream decompressedFileStream = File.Create(newFileName))
				//{
					originalFileStream.Position = 2;
					//using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
					using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
					{
						StreamReader sr = new StreamReader( decompressionStream, Encoding.UTF8 );

						Console.WriteLine("Name: {0}", fileToDecompress.Name);
						Console.WriteLine("ReadToEnd: {0}", sr.ReadToEnd());
						
						//Console.WriteLine("BaseStream: {0}", decompressionStream.BaseStream);
						//byte[] decompressedBuffer = ReadAllBytesFromStream( decompressionStream );
						//string plainText = Encoding.UTF8.GetString(decompressedBuffer);
						//Console.WriteLine("plainText: {0}", plainText);
						////Encoding.UTF8.GetString(decompressionStream);
						//Console.WriteLine("ReadToEnd: {0}", sr.ReadToEnd());
					}

					//using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress, false ))
					//{
					//	var sr = new StreamReader( decompressionStream );
					//
					//	Console.WriteLine("Name: {0}", fileToDecompress.Name);
					//	Console.WriteLine("BaseStream: {0}", decompressionStream.BaseStream);
					//	Console.WriteLine("ReadToEnd: {0}", sr.ReadToEnd());
					//}
				//}
			}
		}

		private static byte[] ReadAllBytesFromStream(Stream stream)
		{
			MemoryStream ret = new MemoryStream();

			Byte[] buffer = new Byte[2048];
			int size;

			while (true)
			{
				size = stream.Read(buffer, 0, buffer.Length);
				if (size > 0)
				{
					ret.Write(buffer, 0, size);
				}
				else
				{
					break;
				}
			}

			ret.Flush();
			ret.Close();
			return ret.ToArray();
		}

		private static void DecompressToPath( string _targetPath, string _zipPath, string _extractPath )
		{
            //ZipFile.CreateFromDirectory(_targetPath, _zipPath);
			
            //ZipFile.ExtractToDirectory(_zipPath, _extractPath);

			//string zipPath = @"c:\example\start.zip";
            //string extractPath = @"c:\example\extract";

            using (ZipArchive archive = ZipFile.OpenRead(_zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
					if( "META-INF/container.xml" == entry.FullName )
					{
						Stream fileStream = entry.Open();
						
						StreamReader sr = new StreamReader( fileStream, Encoding.UTF8 );
						Console.WriteLine("Name: {0}", entry.FullName);
						Console.WriteLine("ReadToEnd: {0}", sr.ReadToEnd());
					}

                    //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    //{
					//	//FileStream
					//
                    //    //entry.ExtractToFile(Path.Combine(_extractPath, entry.FullName));
                    //}
                }
            } 
		}
	}
}

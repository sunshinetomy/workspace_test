using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_MXL
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//Program.TestPath();

				string projectPath = Directory.GetCurrentDirectory() + "\\..\\..\\";
				string resourcePath = projectPath + "Resources\\";
				string musicXmlName = "Dichterliebe01.mxl";

				MusicXml.Domain.Score score = MusicXml.MusicXmlParser.GetScore( musicXmlName );
				for(int index = 0 ; index < score.Parts.Count ; ++index )
				{
					Console.WriteLine( "Name {0}", score.Parts[index].Name );
				}

				MusicXml.Domain.Part mainPart = score.Parts[0];
				for( int index = 0 ; index < mainPart.Measures.Count ; ++index )
				{
					List<MusicXml.Domain.MeasureElement> measureElement = mainPart.Measures[index].MeasureElements;


					//Console.WriteLine( "Name {0}", mainPart.Measures[index].MeasureElements );
				}

				//Program.ReadMusicXml( resourcePath + musicXmlName );

				string txtName = "Test_do.txt";
				
				//string decompressName = "new.gz";
				string decompressName = "Dichterliebe01.mxl";
				
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

				//string noteData = DecompressToPath( decompressPath + "zipFolder", decompressPath + zipName, decompressPath + "extractFolder" );

				//loadXML(decompressPath + decompressName);
			}
			catch( Exception _exception )
			{
				Console.WriteLine("Exception: {0}", _exception);
				// Could not compress.
			}

			Console.ReadLine();
		}

		private static void TestPath()
		{
			Console.WriteLine("TestPath: {0}", Directory.GetCurrentDirectory());
			Console.WriteLine("TestPath: {0}", Directory.GetCurrentDirectory() + "\\..\\..");
			string[] files = Directory.GetFiles( Directory.GetCurrentDirectory() + "\\..\\.." );
			for( int index = 0 ; index < files.Length ; ++index )
			{
				Console.WriteLine("TestPath: {0}", files[index] );
			}
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

		private static string DecompressToPath( string _targetPath, string _zipPath, string _extractPath )
		{
			using (ZipArchive archive = ZipFile.OpenRead(_zipPath))
            {
				string notedataName = null;

				foreach (ZipArchiveEntry entry in archive.Entries)
                {
					if( "META-INF/container.xml" == entry.FullName )
					{
						Stream fileStream = entry.Open();
						StreamReader sr = new StreamReader( fileStream, Encoding.UTF8 );

						string containerData = sr.ReadToEnd();

						Console.WriteLine("Name: {0}", entry.FullName);
						Console.WriteLine("ReadToEnd: {0}", containerData);

						using (XmlReader reader = XmlReader.Create(new StringReader(containerData)))
						{
							
							XmlWriterSettings ws = new XmlWriterSettings();
							ws.Indent = true;
							// Parse the file and display each of the nodes.
							while (reader.Read())
							{
								switch (reader.NodeType)
								{
									case XmlNodeType.Element:
										Console.WriteLine("Element reader.Name: {0}", reader.Name);
										Console.WriteLine("Element reader.Value: {0}", reader.Value);
										Console.WriteLine("Element reader.AttributeCount: {0}", reader.AttributeCount);
										if( true == reader.MoveToFirstAttribute() )
										{
											Console.WriteLine("Element reader.FirstAttributeName: {0}", reader.Name);
											Console.WriteLine("Element reader.FirstAttributeValue: {0}", reader.Value);
											notedataName = reader.Value;
										}
										break;
									case XmlNodeType.Text:
										Console.WriteLine("Text reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.XmlDeclaration:
									case XmlNodeType.ProcessingInstruction:
										Console.WriteLine("XmlDeclaration reader.Name: {0}", reader.Name);
										Console.WriteLine("XmlDeclaration reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.Comment:
										Console.WriteLine("Comment reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.EndElement:
										Console.WriteLine("EndElement");
										break;
								}
							}

							reader.Close();
						}

						//Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}", "Depth", "Name", "Type", "ReadString");
						//Console.WriteLine("{0,-15}{0,-15}{0,-15}{0,-15}", "====");
						//
						//XmlReader xmlReader = XmlReader.Create( new StringReader( containerData ) );
						//while( true == xmlReader.Read() )
						//{
						//	Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}",
						//	xmlReader.Depth, xmlReader.Name, xmlReader.NodeType.ToString(), xmlReader.Value );
						//}
						//
						//xmlReader.Close();

						//XmlDocument xml = new XmlDocument();
						//xml.LoadXml(containerData);
						//XmlNodeList xnList = xml.GetElementsByTagName("container"); //접근할 노드
						//
						//Console.WriteLine("XmlCount: {0}", xnList.Count);
						//foreach (XmlNode xn in xnList)
						//{
						//	Console.WriteLine("XmlNode: {0}", xn["rootfiles"].InnerXml);
						//	Console.WriteLine("XmlNode: {0}", xn["rootfiles"]["rootfile"].InnerText);
						//	//string lng = xn["point"]["y"].InnerText;
						//}
					}
                }

				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					if (notedataName == entry.FullName)
					{
						Stream fileStream = entry.Open();
						StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);

						string noteData = sr.ReadToEnd();

						Console.WriteLine("===================================================");
						Console.WriteLine("Name: {0}", entry.FullName);
						//Console.WriteLine("ReadToEnd: {0}", noteData);

						//XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
						using (XmlReader reader = XmlReader.Create(new StringReader(noteData)))
						{
							XmlWriterSettings ws = new XmlWriterSettings();
							ws.Indent = true;
							// Parse the file and display each of the nodes.
							while (reader.Read())
							{
								switch (reader.NodeType)
								{
									case XmlNodeType.Element:
										Console.WriteLine("Element reader.Name: {0}", reader.Name);
										Console.WriteLine("Element reader.Value: {0}", reader.Value);
										Console.WriteLine("Element reader.AttributeCount: {0}", reader.AttributeCount);
										if (true == reader.MoveToFirstAttribute())
										{
											Console.WriteLine("Element reader.FirstAttributeName: {0}", reader.Name);
											Console.WriteLine("Element reader.FirstAttributeValue: {0}", reader.Value);
											notedataName = reader.Value;
										}
										break;
									case XmlNodeType.Text:
										Console.WriteLine("Text reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.XmlDeclaration:
									case XmlNodeType.ProcessingInstruction:
										Console.WriteLine("XmlDeclaration reader.Name: {0}", reader.Name);
										Console.WriteLine("XmlDeclaration reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.Comment:
										Console.WriteLine("Comment reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.EndElement:
										Console.WriteLine("EndElement");
										break;
								}
							}
							reader.Close();
						}
					}
				}
			}
			return null;
		}

		private static void ReadMusicXmlDocument( string _path )
		{
			//XmlNodeList xnList = xml.GetElementsByTagName("container"); //접근할 노드
			//
			//Console.WriteLine("XmlCount: {0}", xnList.Count);
			//foreach (XmlNode xn in xnList)
			//{
			//	Console.WriteLine("XmlNode: {0}", xn["rootfiles"].InnerXml);
			//	Console.WriteLine("XmlNode: {0}", xn["rootfiles"]["rootfile"].InnerText);
			//	//string lng = xn["point"]["y"].InnerText;
			//}
		}

		public static string ReadMusicXml( string _path )
		{
			using (ZipArchive archive = ZipFile.OpenRead(_path))
            {
				string notedataName = null;

				foreach (ZipArchiveEntry entry in archive.Entries)
                {
					if( "META-INF/container.xml" == entry.FullName )
					{
						Stream fileStream = entry.Open();
						StreamReader sr = new StreamReader( fileStream, Encoding.UTF8 );

						string containerData = sr.ReadToEnd();

						Console.WriteLine("Name: {0}", entry.FullName);
						Console.WriteLine("ReadToEnd: {0}", containerData);

						using (XmlReader reader = XmlReader.Create(new StringReader(containerData)))
						{
							
							XmlWriterSettings ws = new XmlWriterSettings();
							ws.Indent = true;
							// Parse the file and display each of the nodes.
							while (reader.Read())
							{
								switch (reader.NodeType)
								{
									case XmlNodeType.Element:
										Console.WriteLine("Element reader.Name: {0}", reader.Name);
										Console.WriteLine("Element reader.Value: {0}", reader.Value);
										Console.WriteLine("Element reader.AttributeCount: {0}", reader.AttributeCount);
										if( true == reader.MoveToFirstAttribute() )
										{
											Console.WriteLine("Element reader.FirstAttributeName: {0}", reader.Name);
											Console.WriteLine("Element reader.FirstAttributeValue: {0}", reader.Value);
											notedataName = reader.Value;
										}
										break;
									case XmlNodeType.Text:
										Console.WriteLine("Text reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.XmlDeclaration:
									case XmlNodeType.ProcessingInstruction:
										Console.WriteLine("XmlDeclaration reader.Name: {0}", reader.Name);
										Console.WriteLine("XmlDeclaration reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.Comment:
										Console.WriteLine("Comment reader.Value: {0}", reader.Value);
										break;
									case XmlNodeType.EndElement:
										Console.WriteLine("EndElement");
										break;
								}
							}

							reader.Close();
						}
					}
                }

				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					if (notedataName == entry.FullName)
					{
						Stream fileStream = entry.Open();
						StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);

						string noteData = sr.ReadToEnd();

						Console.WriteLine("===================================================");
						Console.WriteLine("Name: {0}", entry.FullName);
						//Console.WriteLine("ReadToEnd: {0}", noteData);

						//XmlReaderSettings settings = new XmlReaderSettings();
						//settings.DtdProcessing = DtdProcessing.Parse;
						//using (XmlReader reader = XmlReader.Create(new StringReader(noteData), settings))
						//{
						//	XmlWriterSettings ws = new XmlWriterSettings();
						//	ws.Indent = true;
						//	// Parse the file and display each of the nodes.
						//	while (reader.Read())
						//	{
						//		switch (reader.NodeType)
						//		{
						//			case XmlNodeType.Element:
						//				Console.WriteLine("Element reader.Name: {0}", reader.Name);
						//				Console.WriteLine("Element reader.Value: {0}", reader.Value);
						//				Console.WriteLine("Element reader.AttributeCount: {0}", reader.AttributeCount);
						//				if (true == reader.MoveToFirstAttribute())
						//				{
						//					Console.WriteLine("Element reader.FirstAttributeName: {0}", reader.Name);
						//					Console.WriteLine("Element reader.FirstAttributeValue: {0}", reader.Value);
						//					notedataName = reader.Value;
						//				}
						//				break;
						//			case XmlNodeType.Text:
						//				Console.WriteLine("Text reader.Value: {0}", reader.Value);
						//				break;
						//			case XmlNodeType.XmlDeclaration:
						//			case XmlNodeType.ProcessingInstruction:
						//				Console.WriteLine("XmlDeclaration reader.Name: {0}", reader.Name);
						//				Console.WriteLine("XmlDeclaration reader.Value: {0}", reader.Value);
						//				break;
						//			case XmlNodeType.Comment:
						//				Console.WriteLine("Comment reader.Value: {0}", reader.Value);
						//				break;
						//			case XmlNodeType.EndElement:
						//				Console.WriteLine("EndElement");
						//				break;
						//		}
						//	}
						//	reader.Close();
						//}

						return noteData;
					}
				}
			}

			return null;
		}
	}
}

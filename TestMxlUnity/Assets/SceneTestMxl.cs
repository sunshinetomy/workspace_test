using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ionic.Zip;
using Ionic.Crc;
using MusicXml.Domain;


public class SceneTestMxl : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Debug.Log( "SceneTestMxl - Start" );

		SceneTestMxl.ReadMusicXml( "" );

		Debug.Log( "SceneTestMxl - End" );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static string ReadMusicXml(string _path)
	{
		string noteDataName = null;

		ZipFile zipfile = new ZipFile(Application.streamingAssetsPath + "/89280-Twinkle_Twinkle_Little_Star.mxl", System.Text.Encoding.UTF8);
		IList<string> entryFileNames = new List<string>(zipfile.EntryFileNames);
		for (int index = 0; index < entryFileNames.Count; ++index)
		{
			Debug.Log("EntryFileName " + entryFileNames[index]);
		
			if( "META-INF/container.xml" == entryFileNames[index] )
			{
				//MemoryStream stream = new MemoryStream();
				noteDataName = entryFileNames[index];
				ZipEntry zipEntry = zipfile[index];
				CrcCalculatorStream stream = zipEntry.OpenReader();
				StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
				//Stream fileStream = zipEntry.InputStream;
				//StreamReader sr = new StreamReader(fileStream, System.Text.Encoding.UTF8);

				string containerData = sr.ReadToEnd();

				Debug.Log("Name: " + zipEntry.FileName);
				Debug.Log("ReadToEnd: " + containerData);
			}
		}

		//using (ZipArchive archive = ZipFile.OpenRead(_path))
		//{
		//	string notedataName = null;
		//
		//	foreach (ZipArchiveEntry entry in archive.Entries)
		//	{
		//		if ("META-INF/container.xml" == entry.FullName)
		//		{
		//			Stream fileStream = entry.Open();
		//			StreamReader sr = new StreamReader(fileStream, System.Text.Encoding.UTF8);
		//
		//			string containerData = sr.ReadToEnd();
		//
		//			Console.WriteLine("Name: {0}", entry.FullName);
		//			Console.WriteLine("ReadToEnd: {0}", containerData);
		//
		//			using (XmlReader reader = XmlReader.Create(new StringReader(containerData)))
		//			{
		//
		//				XmlWriterSettings ws = new XmlWriterSettings();
		//				ws.Indent = true;
		//				// Parse the file and display each of the nodes.
		//				while (reader.Read())
		//				{
		//					switch (reader.NodeType)
		//					{
		//						case XmlNodeType.Element:
		//							Console.WriteLine("Element reader.Name: {0}", reader.Name);
		//							Console.WriteLine("Element reader.Value: {0}", reader.Value);
		//							Console.WriteLine("Element reader.AttributeCount: {0}", reader.AttributeCount);
		//							if (true == reader.MoveToFirstAttribute())
		//							{
		//								Console.WriteLine("Element reader.FirstAttributeName: {0}", reader.Name);
		//								Console.WriteLine("Element reader.FirstAttributeValue: {0}", reader.Value);
		//								notedataName = reader.Value;
		//							}
		//							break;
		//						case XmlNodeType.Text:
		//							Console.WriteLine("Text reader.Value: {0}", reader.Value);
		//							break;
		//						case XmlNodeType.XmlDeclaration:
		//						case XmlNodeType.ProcessingInstruction:
		//							Console.WriteLine("XmlDeclaration reader.Name: {0}", reader.Name);
		//							Console.WriteLine("XmlDeclaration reader.Value: {0}", reader.Value);
		//							break;
		//						case XmlNodeType.Comment:
		//							Console.WriteLine("Comment reader.Value: {0}", reader.Value);
		//							break;
		//						case XmlNodeType.EndElement:
		//							Console.WriteLine("EndElement");
		//							break;
		//					}
		//				}
		//
		//				reader.Close();
		//			}
		//		}
		//	}
		//
		//	foreach (ZipArchiveEntry entry in archive.Entries)
		//	{
		//		if (notedataName == entry.FullName)
		//		{
		//			Stream fileStream = entry.Open();
		//			StreamReader sr = new StreamReader(fileStream, System.Text.Encoding.UTF8);
		//
		//			string noteData = sr.ReadToEnd();
		//
		//			Console.WriteLine("===================================================");
		//			Console.WriteLine("Name: {0}", entry.FullName);
		//			//Console.WriteLine("ReadToEnd: {0}", noteData);
		//
		//			//XmlReaderSettings settings = new XmlReaderSettings();
		//			//settings.DtdProcessing = DtdProcessing.Parse;
		//			//using (XmlReader reader = XmlReader.Create(new StringReader(noteData), settings))
		//			//{
		//			//	XmlWriterSettings ws = new XmlWriterSettings();
		//			//	ws.Indent = true;
		//			//	// Parse the file and display each of the nodes.
		//			//	while (reader.Read())
		//			//	{
		//			//		switch (reader.NodeType)
		//			//		{
		//			//			case XmlNodeType.Element:
		//			//				Console.WriteLine("Element reader.Name: {0}", reader.Name);
		//			//				Console.WriteLine("Element reader.Value: {0}", reader.Value);
		//			//				Console.WriteLine("Element reader.AttributeCount: {0}", reader.AttributeCount);
		//			//				if (true == reader.MoveToFirstAttribute())
		//			//				{
		//			//					Console.WriteLine("Element reader.FirstAttributeName: {0}", reader.Name);
		//			//					Console.WriteLine("Element reader.FirstAttributeValue: {0}", reader.Value);
		//			//					notedataName = reader.Value;
		//			//				}
		//			//				break;
		//			//			case XmlNodeType.Text:
		//			//				Console.WriteLine("Text reader.Value: {0}", reader.Value);
		//			//				break;
		//			//			case XmlNodeType.XmlDeclaration:
		//			//			case XmlNodeType.ProcessingInstruction:
		//			//				Console.WriteLine("XmlDeclaration reader.Name: {0}", reader.Name);
		//			//				Console.WriteLine("XmlDeclaration reader.Value: {0}", reader.Value);
		//			//				break;
		//			//			case XmlNodeType.Comment:
		//			//				Console.WriteLine("Comment reader.Value: {0}", reader.Value);
		//			//				break;
		//			//			case XmlNodeType.EndElement:
		//			//				Console.WriteLine("EndElement");
		//			//				break;
		//			//		}
		//			//	}
		//			//	reader.Close();
		//			//}
		//
		//			return noteData;
		//		}
		//	}
		//}

		return null;
	}

	private static void ReadMusicXml(Score _score)
	{
		//for (int index = 0; index < _score.Parts.Count; ++index)
		//{
		//	Console.WriteLine("Name {0}", _score.Parts[index].Name);
		//}
		//
		//Part mainPart = _score.Parts[0];
		//for (int index = 0; index < mainPart.Measures.Count; ++index)
		//{
		//	List<MeasureElement> measureElementList = mainPart.Measures[index].MeasureElements;
		//	for (int noteIndex = 0; noteIndex < measureElementList.Count; ++noteIndex)
		//	{
		//		MeasureElement measureElement = measureElementList[noteIndex];
		//
		//		/// <chord/> 화음
		//		/// <staff/> 메인 반주
		//		/// <rest/> 쉼
		//		/// ? 잇단음표
		//		/// ? 이음줄
		//
		//		try
		//		{
		//			switch (measureElement.Type)
		//			{
		//				case MeasureElementType.Note:
		//					Note note = measureElement.Element as Note;
		//					Console.WriteLine("Note Octave {0}", note.Pitch.Octave);
		//					Console.WriteLine("Note Step {0}", note.Pitch.Step);
		//					break;
		//
		//				case MeasureElementType.Backup:
		//					Backup backup = measureElement.Element as Backup;
		//					Console.WriteLine("Backup Duration {0}", backup.Duration);
		//					break;
		//
		//				case MeasureElementType.Forward:
		//					Forward forward = measureElement.Element as Forward;
		//					Console.WriteLine("Forward Duration {0}", forward.Duration);
		//					break;
		//			}
		//		}
		//		catch (Exception _e)
		//		{
		//			Console.WriteLine("exception {0}", _e);
		//			Console.WriteLine("index {0}", index);
		//			Console.WriteLine("noteIndex {0}", noteIndex);
		//		}
		//	}
		//}
	}
}

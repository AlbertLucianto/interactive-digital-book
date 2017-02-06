using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;

namespace URECA
{
	public class XMLDecoder
	{
		static List<ObjectXML> listObjects = new List<ObjectXML> ();

		public XMLDecoder ()
		{
		}

		public static void getData(string xmlPath){
			var settings = new XmlReaderSettings ();
			settings.ProhibitDtd = false;
			settings.ValidationType = ValidationType.DTD;
			//settings.DtdProcessing = DtdProcessing.Parse;
			settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler (ValidationCallBack);
			//Console.WriteLine(settings.IgnoreProcessingInstructions);
			settings.IgnoreWhitespace = true;

			XmlReader reader = XmlReader.Create(xmlPath, settings);
			XDocument doc = XDocument.Load(reader);
			var elements = doc.Descendants("page").Elements();

			foreach ( var element in elements )
			{
				if (element.Name.LocalName.Equals("text")) {
					addTextXML (element);
				}
				else if (element.Name.LocalName.Equals("image"))
				{
					addImageXML(element);
				}

				else if (element.Name.LocalName.Equals("video"))
				{
					addVideoXML(element);
				}

				else if (element.Name.LocalName.Equals("model"))
				{
					addModelXML(element);
				}
			}

			//return listObjects;
		}

		private static void ValidationCallBack(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Warning)
				Console.WriteLine("Warning: Matching schema not found.  No validation occurred." + e.Message);
			else // Error
				Console.WriteLine("Validation error: " + e.Message);
		}

		public static List<ObjectXML> accessData(){
			return listObjects;
		}

		public static void clearData(){
			for (int i = 0; i < listObjects.Count; i++)
			{
				listObjects.RemoveAt(i);
			}
		}

		public static void addTextXML(XElement textFromXML){
			TextXML textToAdd = new TextXML ();

			textToAdd.setContent (textFromXML.Element ("content").Value);
			textToAdd.setFontFamily (textFromXML.Attribute ("fontFamily").Value);
			//Console.WriteLine (textFromXML.Attribute ("lineSpace").Value);
			textToAdd.setFontStyle (textFromXML.Attribute ("fontStyle").Value);
			textToAdd.setFontSize (Int32.Parse(textFromXML.Attribute ("fontSize").Value));
			textToAdd.setKerning (Int32.Parse(textFromXML.Attribute ("kerning").Value));
			textToAdd.setLineSpace (Int32.Parse(textFromXML.Attribute ("lineSpace").Value));
			textToAdd.setTop(Int32.Parse(textFromXML.Attribute("top").Value));
			textToAdd.setBottom(Int32.Parse(textFromXML.Attribute("bottom").Value));
			textToAdd.setLeft(Int32.Parse(textFromXML.Attribute("left").Value));
			textToAdd.setRight(Int32.Parse(textFromXML.Attribute("right").Value));
			//Console.WriteLine (textFromXML.Element ("content").Value);*/
			listObjects.Add (textToAdd);
		}

		public static void addImageXML(XElement imageFromXML)
		{
			ImageXML imageToAdd = new ImageXML();

			imageToAdd.setSource(imageFromXML.Element("source").Attribute("href").Value);
			imageToAdd.setDescription(imageFromXML.Element("description").Value);
			//imageToAdd.setId(imageFromXML.Attribute("id").Value);
			//Console.WriteLine (textFromXML.Attribute ("lineSpace").Value);
			imageToAdd.setHeight(Int32.Parse(imageFromXML.Attribute("height").Value));
			imageToAdd.setWidth(Int32.Parse(imageFromXML.Attribute("width").Value));
			imageToAdd.setTop(Int32.Parse(imageFromXML.Attribute("top").Value));
			imageToAdd.setBottom(Int32.Parse(imageFromXML.Attribute("bottom").Value));
			imageToAdd.setLeft(Int32.Parse(imageFromXML.Attribute("left").Value));
			imageToAdd.setRight(Int32.Parse(imageFromXML.Attribute("right").Value));
			//Console.WriteLine (textFromXML.Element ("content").Value);*/

			listObjects.Add(imageToAdd);
		}

		public static void addVideoXML(XElement videoFromXML)
		{
			VideoXML videoToAdd = new VideoXML();

			videoToAdd.setSource(videoFromXML.Element("source").Attribute("href").Value);
			videoToAdd.setDescription(videoFromXML.Element("description").Value);
			//videoToAdd.setId(videoFromXML.Attribute("id").Value);
			//Console.WriteLine (textFromXML.Attribute ("lineSpace").Value);
			videoToAdd.setHeight(Int32.Parse(videoFromXML.Attribute("height").Value));
			videoToAdd.setWidth(Int32.Parse(videoFromXML.Attribute("width").Value));
			videoToAdd.setTop(Int32.Parse(videoFromXML.Attribute("top").Value));
			videoToAdd.setBottom(Int32.Parse(videoFromXML.Attribute("bottom").Value));
			videoToAdd.setLeft(Int32.Parse(videoFromXML.Attribute("left").Value));
			videoToAdd.setRight(Int32.Parse(videoFromXML.Attribute("right").Value));
			//Console.WriteLine (textFromXML.Element ("content").Value);*/

			listObjects.Add(videoToAdd);
		}

		public static void addModelXML(XElement modelFromXML)
		{
			ModelXML modelToAdd = new ModelXML();

			modelToAdd.setSource(modelFromXML.Element("source").Attribute("href").Value);
			modelToAdd.setDescription(modelFromXML.Element("description").Value);
			//modelToAdd.setId(modelFromXML.Attribute("id").Value);
			//Console.WriteLine (textFromXML.Attribute ("lineSpace").Value);
			modelToAdd.setHeight(Int32.Parse(modelFromXML.Attribute("height").Value));
			modelToAdd.setWidth(Int32.Parse(modelFromXML.Attribute("width").Value));
			modelToAdd.setTop(Int32.Parse(modelFromXML.Attribute("top").Value));
			modelToAdd.setBottom(Int32.Parse(modelFromXML.Attribute("bottom").Value));
			modelToAdd.setLeft(Int32.Parse(modelFromXML.Attribute("left").Value));
			modelToAdd.setRight(Int32.Parse(modelFromXML.Attribute("right").Value));
			//Console.WriteLine (textFromXML.Element ("content").Value);*/

			listObjects.Add(modelToAdd);
		}
	}
}


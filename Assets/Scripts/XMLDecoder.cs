using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;

using UnityEngine;

namespace URECA
{
	public class XMLDecoder
	{
		static List<ObjectXML> listObjects = new List<ObjectXML> ();

		static float positionX, positionY, positionZ;
		static float scaleX, scaleY, scaleZ;
		static float rotationX, rotationY, rotationZ;

		static ObjectXML addToList; //Every child of ObjectXML will be passed, then set Transform attributes

		public XMLDecoder ()
		{
		}

		public static void loadData(string xmlPath){
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

			//===============Exporting XML to C# Classes===============

			foreach ( var element in elements )
			{
				
				if (element.Name.LocalName.Equals("text")) {
					addToList = getTextXML (element);
				}
				else if (element.Name.LocalName.Equals("image"))
				{
					addToList = getImageXML(element);
				}

				else if (element.Name.LocalName.Equals("video"))
				{
					addToList = getVideoXML(element);
				}

				else if (element.Name.LocalName.Equals("model"))
				{
					addToList = getModelXML(element);
				}

				addToList.setId (element.Attribute ("id").Value);

				positionX = float.Parse (element.Attribute ("positionX").Value);
				positionY = float.Parse (element.Attribute ("positionY").Value);
				positionZ = float.Parse (element.Attribute ("positionZ").Value);

				scaleX = float.Parse (element.Attribute ("scaleX").Value);
				scaleY = float.Parse (element.Attribute ("scaleY").Value);
				scaleZ = float.Parse (element.Attribute ("scaleZ").Value);

				rotationX = float.Parse (element.Attribute ("rotationX").Value);
				rotationY = float.Parse (element.Attribute ("rotationY").Value);
				rotationZ = float.Parse (element.Attribute ("rotationZ").Value);

				addToList.setPosition (positionX, positionY, positionZ);
				addToList.setScale (scaleX, scaleY, scaleZ);
				addToList.setRotation (rotationX, rotationY, rotationZ);

				listObjects.Add(addToList);
			}

		}

		private static void ValidationCallBack(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Warning)
				Console.WriteLine("Warning: Matching schema not found.  No validation occurred." + e.Message);
			else // Error
				Console.WriteLine("Validation error: " + e.Message);
		}



		//=================== Return List of Objects ===================
		public static List<ObjectXML> getData(){
			return listObjects;
		}



		//=================== Clear List / Buffer ======================
		public static void clearData(){
			for (int i = 0; i < listObjects.Count; i++)
			{
				listObjects.RemoveAt(i);
			}
		}
			


		//============ Methods for retrieving XML of each type ==========

		internal static TextXML getTextXML(XElement textFromXML){
			TextXML textToAdd = new TextXML ();

			textToAdd.setContent (textFromXML.Element ("content").Value);
			textToAdd.setFont (textFromXML.Attribute ("font").Value);
			textToAdd.setFontStyle (textFromXML.Attribute ("fontStyle").Value);
			textToAdd.setFontSize (Int32.Parse(textFromXML.Attribute ("fontSize").Value));
			textToAdd.setLineSpace (Int32.Parse(textFromXML.Attribute ("lineSpace").Value));

			return textToAdd;
		}

		internal static ImageXML getImageXML(XElement imageFromXML)
		{
			ImageXML imageToAdd = new ImageXML();

			imageToAdd.setSource(imageFromXML.Element("source").Attribute("href").Value);
			imageToAdd.setDescription(imageFromXML.Element("description").Value);

			return imageToAdd;
		}

		internal static VideoXML getVideoXML(XElement videoFromXML)
		{
			VideoXML videoToAdd = new VideoXML();

			videoToAdd.setSource(videoFromXML.Element("source").Attribute("href").Value);
			videoToAdd.setDescription(videoFromXML.Element("description").Value);
			videoToAdd.setHeight(Int32.Parse(videoFromXML.Attribute("height").Value));
			videoToAdd.setWidth(Int32.Parse(videoFromXML.Attribute("width").Value));

			return videoToAdd;
		}

		internal static ModelXML getModelXML(XElement modelFromXML)
		{
			ModelXML modelToAdd = new ModelXML();

			modelToAdd.setSource(modelFromXML.Element("source").Attribute("href").Value);
			modelToAdd.setDescription(modelFromXML.Element("description").Value);

			return modelToAdd;
		}
	}
}


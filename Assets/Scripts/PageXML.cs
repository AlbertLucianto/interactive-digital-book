using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;

using UnityEngine;

namespace URECA
{
	public class PageXML
	{
		public List<ObjectXML> listObjects = new List<ObjectXML> ();
		public int pageNum;

		public float positionX, positionY, positionZ;
		float scaleX, scaleY, scaleZ;
		float rotationX, rotationY, rotationZ;

		ObjectXML addToList; //Every child of ObjectXML will be passed, then set Transform attributes

//		static object[] arguments = new object[1];

		public PageXML(int num){
			pageNum = num;
		}

		public void construct(XElement page){

			var elements = page.Elements ();

			foreach ( XElement element in elements )
			{

				//--------------Call Method by String------------
				//				string stringMethod = "get" + element.Name.LocalName + "XML";
				//				MethodInfo callMethod = typeof (XMLDecoder).GetMethod (stringMethod);
				//				arguments [0] = element;
				//				callMethod.Invoke (null, XMLDecoder.arguments);

				if (element.Name.LocalName.Equals("text")) {
					addToList = gettextXML (element);
				}
				else if (element.Name.LocalName.Equals("image"))
				{
					addToList = getimageXML(element);
				}

				else if (element.Name.LocalName.Equals("video"))
				{
					addToList = getvideoXML(element);
				}

				else if (element.Name.LocalName.Equals("model"))
				{
					addToList = getmodelXML(element);
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

		//============ Static Methods for retrieving XML of each type ==========

		internal static TextXML gettextXML(XElement textFromXML){
			TextXML textToAdd = new TextXML ();

			textToAdd.setContent (textFromXML.Element ("content").Value);
			textToAdd.setFont (textFromXML.Attribute ("font").Value);
			textToAdd.setFontStyle (textFromXML.Attribute ("fontStyle").Value);
			textToAdd.setFontSize (Int32.Parse(textFromXML.Attribute ("fontSize").Value));
			textToAdd.setLineSpace (Int32.Parse(textFromXML.Attribute ("lineSpace").Value));

			return textToAdd;
		}

		internal static ImageXML getimageXML(XElement imageFromXML)
		{
			ImageXML imageToAdd = new ImageXML();

			imageToAdd.setSource(imageFromXML.Element("source").Attribute("href").Value);
			imageToAdd.setDescription(imageFromXML.Element("description").Value);

			return imageToAdd;
		}

		internal static VideoXML getvideoXML(XElement videoFromXML)
		{
			VideoXML videoToAdd = new VideoXML();

			videoToAdd.setSource(videoFromXML.Element("source").Attribute("href").Value);
			videoToAdd.setDescription(videoFromXML.Element("description").Value);

			return videoToAdd;
		}

		internal static ModelXML getmodelXML(XElement modelFromXML)
		{
			ModelXML modelToAdd = new ModelXML();

			modelToAdd.setSource(modelFromXML.Element("source").Attribute("href").Value);
			modelToAdd.setDescription(modelFromXML.Element("description").Value);

			return modelToAdd;
		}

	}

}


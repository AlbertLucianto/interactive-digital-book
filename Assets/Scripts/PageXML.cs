using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Reflection;

using UnityEngine;

namespace URECA
{
	public class PageXML
	{
		[XmlArray("page")]
		[XmlArrayItem("text", typeof(TextXML))]
		[XmlArrayItem("image", typeof(ImageXML))]
		[XmlArrayItem("video", typeof(VideoXML))]
		[XmlArrayItem("model", typeof(ModelXML))]
		public List<ObjectXML> listObjects = new List<ObjectXML> ();
		[XmlIgnore]
		public int pageNum;

		ObjectXML addToList; //Every child of ObjectXML will be passed, then set Transform attributes

//		static object[] arguments = new object[1];

		public PageXML(){}

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

				addToList.setPositionX(float.Parse (element.Attribute ("positionX").Value));
				addToList.setPositionY(float.Parse (element.Attribute ("positionY").Value));
				addToList.setPositionZ(float.Parse (element.Attribute ("positionZ").Value));

				addToList.setScaleX(float.Parse (element.Attribute ("scaleX").Value));
				addToList.setScaleY(float.Parse (element.Attribute ("scaleY").Value));
				addToList.setScaleZ(float.Parse (element.Attribute ("scaleZ").Value));

				addToList.setRotationX(float.Parse (element.Attribute ("rotationX").Value));
				addToList.setRotationY(float.Parse (element.Attribute ("rotationY").Value));
				addToList.setRotationZ(float.Parse (element.Attribute ("rotationZ").Value));

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


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace URECA
{
	[XmlInclude(typeof(ObjectXML))]
	public class TextXML: ObjectXML
	{
		private string content;
		[XmlAttribute("font")]
		private string font;
		[XmlAttribute("fontStyle")]
		private string fontStyle;
		[XmlAttribute("fontSize")]
		private float fontSize;
		[XmlAttribute("lineSpace")]
		private float lineSpace;

		public override GameObject instantiateXMLObject()
		{
			GameObject textUnity = new GameObject ();
			textUnity.hideFlags = HideFlags.HideInHierarchy; //hide the object in scene

			textUnity.AddComponent<Text>();
			textUnity.AddComponent<ContentSizeFitter>();
			textUnity.AddComponent<ColliderTextFitterSize> ();
			textUnity.tag = "Text";

			Text addText = textUnity.GetComponent<Text> ();
			ContentSizeFitter textFitter = textUnity.GetComponent<ContentSizeFitter>();

			addText.text = content;

			if (font.Equals ("Arial")) {
				addText.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			}
			else addText.font = Resources.Load ("Fonts/" + font, typeof(Font)) as Font;

			textFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			textFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			return textUnity;
		}

		public void setContent(string inContent){
			content = Regex.Replace(inContent,@"[\t]+|/  +/",string.Empty).Trim();
		}

		public void setFont(string inFont){
			font = inFont;
		}

		public void setFontStyle(string inFontStyle){
			fontStyle = inFontStyle;
		}

		public void setFontSize(float inFontSize){
			fontSize = inFontSize;
		}

		public void setLineSpace(float inLineSpace){
			lineSpace = inLineSpace;
		}

		public string getContent(){
			return content;
		}

		public string getFont(){
			return font;
		}

		public string getFontStyle(){
			return fontStyle;
		}

		public float getFontSize(){
			return fontSize;
		}

		public float getLineSpace(){
			return lineSpace;
		}
	}
}


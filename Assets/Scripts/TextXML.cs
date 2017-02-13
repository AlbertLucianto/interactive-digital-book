using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class TextXML: ObjectXML
	{
		private string content;
		private string font;
		private string fontStyle;
		private int fontSize;
		private int lineSpace;

		public override GameObject instantiateXMLObject()
		{
			GameObject textUnity = new GameObject ();
			textUnity.hideFlags = HideFlags.HideInHierarchy; //hide the object in scene

			textUnity.AddComponent<Text>();
			textUnity.AddComponent<ContentSizeFitter>();

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
			content = inContent;
		}

		public void setFont(string inFont){
			font = inFont;
		}

		public void setFontStyle(string inFontStyle){
			fontStyle = inFontStyle;
		}

		public void setFontSize(int inFontSize){
			fontSize = inFontSize;
		}

		public void setLineSpace(int inLineSpace){
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

		public int getFontSize(){
			return fontSize;
		}

		public int getLineSpace(){
			return lineSpace;
		}
	}
}


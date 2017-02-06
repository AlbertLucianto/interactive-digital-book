using System;

namespace URECA
{
	public class TextXML: ObjectXML
	{
		private string content;
		private string fontFamily;
		private string fontStyle;
		private int fontSize;
		private int kerning;
		private int lineSpace;

		public TextXML (){
			
		}

		public void setContent(string inContent){
			content = inContent;
		}

		public void setFontFamily(string inFontFamily){
			fontFamily = inFontFamily;
		}

		public void setFontStyle(string inFontStyle){
			fontStyle = inFontStyle;
		}

		public void setFontSize(int inFontSize){
			fontSize = inFontSize;
		}

		public void setKerning(int inKerning){
			kerning = inKerning;
		}

		public void setLineSpace(int inLineSpace){
			lineSpace = inLineSpace;
		}

		public string getContent(){
			return content;
		}

		public string getFontFamily(){
			return fontFamily;
		}

		public string getFontStyle(){
			return fontStyle;
		}

		public int getFontSize(){
			return fontSize;
		}

		public int getKerning(){
			return kerning;
		}

		public int getLineSpace(){
			return lineSpace;
		}
	}
}


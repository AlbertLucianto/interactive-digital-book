using System;

namespace URECA
{
	class ImageXML: ObjectXML
	{
		private string source;
		private string description;
		private int height;
		private int width;

		public ImageXML()
		{

		}

		public void setSource(string inSource)
		{
			source = inSource;
		}

		public void setDescription(string inDescription)
		{
			description = inDescription;
		}

		public void setHeight(int inHeight)
		{
			height = inHeight;
		}

		public void setWidth(int inWidth)
		{
			width = inWidth;
		}

		public string getSource()
		{
			return source;
		}

		public string getDescription()
		{
			return description;
		}

		public int getHeight()
		{
			return height;
		}

		public int getWidth()
		{
			return width;
		}
	}
}



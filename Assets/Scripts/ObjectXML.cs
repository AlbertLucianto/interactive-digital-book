using System;

namespace URECA
{
	public abstract class ObjectXML
	{
		private string id;
		private int top;
		private int bottom;
		private int left;
		private int right;

		public ObjectXML ()
		{
		}

		public void setId(string inId)
		{
			id = inId;
		}

		public void setTop(int inTop)
		{
			top = inTop;
		}

		public void setBottom(int inBottom)
		{
			bottom = inBottom;
		}

		public void setLeft(int inLeft)
		{
			left = inLeft;
		}

		public void setRight(int inRight)
		{
			right = inRight;
		}
			
		public string getId()
		{
			return id;
		}

		public int getTop()
		{
			return top;
		}

		public int getBottom()
		{
			return bottom;
		}

		public int getLeft()
		{
			return left;
		}
		public int getRight()
		{
			return right;
		}
	}
}


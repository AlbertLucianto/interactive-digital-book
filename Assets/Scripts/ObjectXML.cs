using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace URECA
{
	public abstract class ObjectXML
	{
		[XmlAttribute("id")]
		private string id;
		[XmlAttribute("positionX")]
		public float positionX;
		[XmlAttribute("positionY")]
		public float positionY;
		[XmlAttribute("positionZ")]
		public float positionZ;
		[XmlAttribute("scaleX")]
		public float scaleX;
		[XmlAttribute("scaleY")]
		public float scaleY;
		[XmlAttribute("scaleZ")]
		public float scaleZ;
		[XmlAttribute("rotationX")]
		public float rotationX;
		[XmlAttribute("rotationY")]
		public float rotationY;
		[XmlAttribute("rotationZ")]
		public float rotationZ;
		[XmlIgnore]
		private ArrayList interactions;
		/*private int top;
		private int bottom;
		private int left;
		private int right;*/

		public abstract GameObject instantiateXMLObject();

		//============== ID ==============

		public void setId(string inId)
		{
			id = inId;
		}

		public string getId()
		{
			return id;
		}

		//============== Position ==============

		public void setPositionX(float x=0.0f)
		{
			positionX = x;
		}

		public float getPositionX()
		{
			return positionX;
		}

		public void setPositionY(float y=0.0f)
		{
			positionY = y;
		}

		public float getPositionY()
		{
			return positionY;
		}
		public void setPositionZ(float z=0.0f)
		{
			positionZ = z;
		}

		public float getPositionZ()
		{
			return positionZ;
		}

		//============== Scale ==============

		public void setScaleX(float x=0.0f)
		{
			scaleX = x;
		}

		public float getScaleX()
		{
			return scaleX;
		}

		public void setScaleY(float y=0.0f)
		{
			scaleY = y;
		}

		public float getScaleY()
		{
			return scaleY;
		}
		public void setScaleZ(float z=0.0f)
		{
			scaleZ = z;
		}

		public float getScaleZ()
		{
			return scaleZ;
		}

		//============== Rotation ==============

		public void setRotationX(float x=0.0f)
		{
			rotationX = x;
		}

		public float getRotationX()
		{
			return rotationX;
		}

		public void setRotationY(float y=0.0f)
		{
			rotationY = y;
		}

		public float getRotationY()
		{
			return rotationY;
		}
		public void setRotationZ(float z=0.0f)
		{
			rotationZ = z;
		}

		public float getRotationZ()
		{
			return rotationZ;
		}

		//============== Interactions ==============

		public void addInteractions(string interaction)
		{
			interactions.Add(interaction);
		}

		public void clearInteractions()
		{
			interactions.Clear();
		}

		public ArrayList getInteractions()
		{
			return interactions;
		}
	}
}


using System;
using UnityEngine;
using System.Collections;

namespace URECA
{
	public abstract class ObjectXML
	{
		private string id;
		private Vector3 position;
		private Vector3 scale;
		private Quaternion rotation;
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

		public void setPosition(float x=0.0f, float y=0.0f, float z=0.0f)
		{
			position = new Vector3(x,y,z);
		}

		public Vector3 getPosition()
		{
			return position;
		}

		//============== Scale ==============

		public void setScale(float x=0.0f, float y=0.0f, float z=0.0f)
		{
			scale = new Vector3(x,y,z);
		}

		public Vector3 getScale()
		{
			return scale;
		}

		//============== Rotation ==============

		public void setRotation(float x=0.0f, float y=0.0f, float z=0.0f)
		{
			rotation = Quaternion.Euler(new Vector3(x,y,z));
		}

		public Quaternion getRotation()
		{
			return rotation;
		}

	}
}


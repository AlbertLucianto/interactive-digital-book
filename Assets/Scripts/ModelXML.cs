using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;

namespace URECA
{
	[XmlInclude(typeof(ObjectXML))]
	public class ModelXML: ObjectXML
	{
		[XmlElement("source")]
		[XmlAttribute("href")]
		private string source;
		private string description;

		public override GameObject instantiateXMLObject()
		{
			GameObject modelUnity = GameObject.Instantiate(Resources.Load (source, typeof(GameObject)) as GameObject, new Vector3(this.getPositionX(),this.getPositionY(),this.getPositionZ()), Quaternion.Euler(this.getRotationX(),this.getRotationY(),this.getRotationZ()));
			modelUnity.hideFlags = HideFlags.HideInHierarchy;
			modelUnity.tag = "Model";

			GameObject modelSource = new GameObject();
			modelSource.name = this.getSource ();
			modelSource.tag = "Source";
			modelSource.transform.SetParent (modelUnity.transform, false);

			GameObject modelDesc = new GameObject();
			modelDesc.name = this.getDescription ();
			modelDesc.tag = "Description";
			modelDesc.transform.SetParent (modelUnity.transform, false);

			return modelUnity;
		}

		public void setSource(string inSource)
		{
			source = inSource;
		}

		public void setDescription(string inDescription)
		{
			description = inDescription;
		}

		public string getSource()
		{
			return source;
		}

		public string getDescription()
		{
			return description;
		}

	}
}



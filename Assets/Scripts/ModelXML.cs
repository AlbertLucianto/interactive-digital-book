using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	class ModelXML: ObjectXML
	{
		private string source;
		private string description;

		public override GameObject instantiateXMLObject()
		{
			GameObject modelXML = Resources.Load (source, typeof(GameObject)) as GameObject;

			return modelXML;
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



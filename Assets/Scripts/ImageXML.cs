using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

namespace URECA
{
	class ImageXML: ObjectXML
	{
		private string source;
		private string description;

		public override GameObject instantiateXMLObject()
		{
			GameObject imageUnity = new GameObject ();
			imageUnity.hideFlags = HideFlags.HideInHierarchy; //hide the object in scene

			imageUnity.AddComponent<Image> ();
			//imageUnity.AddComponent<RectTransform> ();

			Image img = imageUnity.GetComponent("Image") as Image;
			RectTransform imgTransform = imageUnity.GetComponent("RectTransform") as RectTransform;

			Texture2D tex = LoadPNG (source);
			img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
			imgTransform.sizeDelta = new Vector2 (tex.width, tex.height);

			Debug.Log ("Add image successful");

			return imageUnity;
		}

		public static Texture2D LoadPNG(string filePath)
		{

			Texture2D tex = null;
			byte[] fileData;

			if (File.Exists(filePath))
			{
				fileData = File.ReadAllBytes(filePath);
				tex = new Texture2D(2, 2);
				tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			}
			return tex;
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



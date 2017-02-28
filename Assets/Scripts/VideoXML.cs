using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

namespace URECA
{
	class VideoXML: ObjectXML
	{
		private string source;
		private string description;

		public VideoXML()
		{

		}
		public override GameObject instantiateXMLObject(){
			GameObject videoUnity = GameObject.CreatePrimitive (PrimitiveType.Plane);
			videoUnity.hideFlags = HideFlags.HideInHierarchy; //hide the object in scene
			videoUnity.tag = "Video";

			videoUnity.AddComponent<RectTransform> ();
			videoUnity.AddComponent<AudioSource> ();

			Renderer getRender = videoUnity.GetComponent<Renderer>();
			AudioSource getAudio = videoUnity.GetComponent<AudioSource> ();

			//Debug.Log (source);
			var www = new WWW("file://" + Application.dataPath + "/" + source);
			//Debug.Log ("file://" + Application.dataPath + source);
			MovieTexture movieTexture = www.movie;

			if (movieTexture.isReadyToPlay) {
				Debug.Log ("Loading movie");
			};
				
			getRender.material.mainTexture = movieTexture;
			getAudio.clip = movieTexture.audioClip;

			Debug.Log ("Add video successful");

			return videoUnity;
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

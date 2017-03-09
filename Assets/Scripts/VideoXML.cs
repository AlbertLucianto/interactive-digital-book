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

//			Collider toRemove = videoUnity.GetComponent<Collider> ();
//			Debug.Log (toRemove);
//			MonoBehaviour.Destroy (toRemove); // Will later use BoxCollider2D for all kind of objects
//			Debug.Log (toRemove);

			Renderer getRender = videoUnity.GetComponent<Renderer>();
			AudioSource getAudio = videoUnity.GetComponent<AudioSource> ();

			var www = new WWW("file://" + Application.dataPath + "/" + source);
			//Debug.Log ("file://" + Application.dataPath + source);
			MovieTexture movieTexture = www.movie;

			if (movieTexture.isReadyToPlay) {
				Debug.Log ("Loading movie");
			};
				
			getRender.material.mainTexture = movieTexture;

//			if (movieTexture.audioClip.loadState == AudioDataLoadState.Loaded) {
//				Debug.Log ("Audio is ready");
//			}

			getAudio.clip = movieTexture.audioClip;

			return videoUnity;
		}

//		public override GameObject instantiateXMLObject(){
//			GameObject videoUnity = GameObject.CreatePrimitive (PrimitiveType.Plane);
//			videoUnity.hideFlags = HideFlags.HideInHierarchy; //hide the object in scene
//			videoUnity.tag = "Video";
//
//			videoUnity.AddComponent<RectTransform> ();
//			videoUnity.AddComponent<AudioSource> ();
//
//			// Debug.Log (source);
//			var www = new WWW("file://" + Application.dataPath + "/" + source);
//			// Debug.Log ("file://" + Application.dataPath + source);
//			MovieTexture movieTexture = www.movie;
//
//			MonoBehaviour toRunIEnumerator;
//
//			return toRunIEnumerator.StartCoroutine(waitLoadMovie (movieTexture, videoUnity));
//		}
//
//		private IEnumerator waitLoadMovie(MovieTexture movieTexture, GameObject videoUnity){
//
//			Renderer getRender = videoUnity.GetComponent<Renderer>();
//			AudioSource getAudio = videoUnity.GetComponent<AudioSource> ();
//
//			while(!movieTexture.isReadyToPlay) {
//				yield return new WaitForSeconds (0.1f);
//			};
//			while(!movieTexture.audioClip.loadState.ToString ().Equals("Loaded")) {
//				yield return new WaitForSeconds (0.1f);
//			}
//
//			getRender.material.mainTexture = movieTexture;
//			getAudio.clip = movieTexture.audioClip;
//
//			yield return videoUnity;
//
//		}


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

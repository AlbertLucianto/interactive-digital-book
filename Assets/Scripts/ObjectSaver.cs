using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class ObjectSaver {

		private static List<PageXML> listSavedPages = new List<PageXML>();

		public static void saveObjects() {	

			listSavedPages.Clear ();
			GameObject canvas = GameObject.FindWithTag ("Canvas");
			int i = 1;
			foreach (Transform child in canvas.transform) {	
				
				PageXML pageToAdd = new PageXML(i++);

				GameObject page = child.gameObject;
				saveObjectsToPage(page, pageToAdd);

				listSavedPages.Add(pageToAdd);

			}
	
		}

		public static void saveObjectsToPage(GameObject page, PageXML pageToAdd){
			foreach (Transform child in page.transform) {
				GameObject unityObject = child.gameObject;
				ObjectXML objectToAdd;
				if (unityObject.tag == "Text") {
					objectToAdd = saveText (unityObject);
				}
				else if (unityObject.tag == "Image") {
					objectToAdd = saveImage (unityObject);
				}
				else if (unityObject.tag == "Video") {
					objectToAdd = saveVideo (unityObject);
				}
				else if (unityObject.tag == "Model") {
					objectToAdd = saveModel (unityObject);
				}
				else continue;
				objectToAdd.setPositionX (unityObject.transform.position.x);
				objectToAdd.setPositionY (unityObject.transform.position.y);
				objectToAdd.setPositionZ (unityObject.transform.position.z);
				objectToAdd.setScaleX (unityObject.transform.localScale.x);
				objectToAdd.setScaleY (unityObject.transform.localScale.y);
				objectToAdd.setScaleZ (unityObject.transform.localScale.z);
				objectToAdd.setRotationX (unityObject.transform.rotation.x);
				objectToAdd.setRotationY (unityObject.transform.rotation.y);
				objectToAdd.setRotationZ (unityObject.transform.rotation.z);
				if (unityObject.GetComponent<Interaction_ObjectRotatorBySound>())
					objectToAdd.addInteractions ("Blow");
				//if (unityObject.GetComponent<Interaction_Zoom>)
				//	objectToAdd.setInteractions ("Zoom");
				pageToAdd.listObjects.Add(objectToAdd);
			}
		}

		internal static TextXML saveText(GameObject unityObject){
			TextXML textToAdd = new TextXML ();

			textToAdd.setContent (unityObject.GetComponent<Text>().text);
			textToAdd.setFont (unityObject.GetComponent<Text>().font.name);
			textToAdd.setFontStyle (unityObject.GetComponent<Text>().fontStyle.ToString());
			textToAdd.setFontSize (unityObject.GetComponent<Text>().fontSize);
			textToAdd.setLineSpace (unityObject.GetComponent<Text>().lineSpacing);

			return textToAdd;
		}

		internal static ImageXML saveImage(GameObject unityObject)
		{
			ImageXML imageToAdd = new ImageXML();

			foreach (Transform child in unityObject.transform) {
				if (child.gameObject.CompareTag("Source"))
					imageToAdd.setSource(child.gameObject.name);
				else if (child.gameObject.CompareTag("Description"))
					imageToAdd.setDescription(child.gameObject.name);
			}

			return imageToAdd;
		}

		internal static VideoXML saveVideo(GameObject unityObject)
		{
			VideoXML videoToAdd = new VideoXML();

			foreach (Transform child in unityObject.transform) {
				if (child.gameObject.CompareTag("Source"))
					videoToAdd.setSource(child.gameObject.name);
				else if (child.gameObject.CompareTag("Description"))
					videoToAdd.setDescription(child.gameObject.name);
			}

			return videoToAdd;
		}

		internal static ModelXML saveModel(GameObject unityObject)
		{
			ModelXML modelToAdd = new ModelXML();

			foreach (Transform child in unityObject.transform) {
				if (child.gameObject.CompareTag("Source"))
					modelToAdd.setSource(child.gameObject.name);
				else if (child.gameObject.CompareTag("Description"))
					modelToAdd.setDescription(child.gameObject.name);
			}

			return modelToAdd;
		}

		public static List<PageXML> getListSavedPages(){
			return listSavedPages;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class ObjectsLoader : MonoBehaviour {

		static PageLoader pageLoader;

		private static List<PageLoader> pages = new List<PageLoader>();
		private static InputField inputFileName;
		private static GameObject canvas = GameObject.FindWithTag ("Canvas");
		private static GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
		private static InputField navPage = GameObject.FindWithTag ("Page").GetComponent<InputField>();

		public static void loadObjects() {

			foreach (Transform child in canvas.transform) {
				//Debug.Log (child.gameObject.name);
				Destroy (child.gameObject);
			}

			inputFileName = GameObject.FindWithTag ("InputFileName").GetComponent<InputField> ();
			string fileName = inputFileName.text;

			mainCamera.transform.position = canvas.transform.position;

			mainCamera.transform.position += new Vector3 (0.0f, 0.0f, -432.0f); //to adjust visible area with canvas

			XMLDecoder.clearData ();

			XMLDecoder.loadData("Assets/Save_files/" + fileName + ".xml");

			foreach (var pageXML in XMLDecoder.getData()) {

				pageLoader = new PageLoader();
				pageLoader.addObjectsToPage (pageXML);
				pages.Add(pageLoader);

			}

			loadPageToWindow (0);
			navPage.text = "1";
		}

		public static void loadPageToWindow(int pageNum){

			//Debug.Log (pages[0]);

			foreach (Transform child in canvas.transform) {

				Destroy (child.gameObject);

			}

			foreach (GameObjectWithTransform theGameObject in pages[pageNum].gameObjects) {

				var x = (GameObject)Instantiate(theGameObject.gameObject) as GameObject;

				if (!x.GetComponent<MeshCollider> ()) {
					x.gameObject.AddComponent<BoxCollider> ();
					BoxCollider collider = x.gameObject.GetComponent<BoxCollider> ();
					collider.isTrigger = true;

					if (x.gameObject.GetComponent<RectTransform> ()) {
						RectTransform rectTransform = x.gameObject.GetComponent<RectTransform> ();
						collider.size = new Vector3 (rectTransform.rect.width, rectTransform.rect.height, 1);
					} else { // Default size
						collider.size = new Vector3 (50f / theGameObject.scale.x, 50f / theGameObject.scale.y, 1 / theGameObject.scale.z);
					}
				} else {
					x.GetComponent<MeshCollider> ().convex = true;
					x.GetComponent<MeshCollider> ().isTrigger = true;
				}

				x.name = theGameObject.id;

				x.transform.position = theGameObject.position ;
				x.transform.localScale = theGameObject.scale;
				x.transform.rotation = theGameObject.rotation;

				x.transform.SetParent(canvas.transform, false);

			}
		}
	}
}
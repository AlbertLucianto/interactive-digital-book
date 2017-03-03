using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class ObjectsLoader : MonoBehaviour {

		static PageLoader pageLoader;
		private static bool destroyFirst;
		private static InputField inputFileName;
		private static GameObject canvas = GameObject.FindWithTag ("Canvas");
		private static GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
		private static InputField navPage = GameObject.FindWithTag ("Page").GetComponent<InputField>();

		public static void loadObjects(MonoBehaviour something) {	// MonoBehaviour something: TRICKY (StartCoroutine needs
																	// an instance to run, but in static method, it cannot
			foreach (Transform child in canvas.transform) {			// contain an instance except it is passed by argument)
																	// i.e. passing any of 'this' of MonoBehaviour will do
				Destroy (child.gameObject);

			}

			inputFileName = GameObject.FindWithTag ("InputFileName").GetComponent<InputField> ();
			string fileName = inputFileName.text;

			mainCamera.transform.position = canvas.transform.position;

			mainCamera.transform.position += new Vector3 (0.0f, 0.0f, -432.0f); //to adjust visible area with canvas

			XMLDecoder.clearData ();

			XMLDecoder.loadData("Assets/Save_files/" + fileName + ".xml");

			something.StartCoroutine (createThePages ());
				
		}

		public static void showPageOnWindow(int pageNum){
			
			foreach (Transform page in canvas.transform) {

				page.gameObject.SetActive (false);
				//Debug.Log ("Hiding " + page.gameObject);

			}

			//Debug.Log ("Showing " + canvas.transform.GetChild (pageNum).gameObject);
			canvas.transform.GetChild (pageNum).gameObject.SetActive (true);
		}

		public static IEnumerator createThePages(){ // IEnumerator used to wait destroy

			while (canvas.transform.childCount > 0)
				yield return new WaitForSeconds (0.1f);

			int i = 1;
			foreach (var pageXML in XMLDecoder.getData()) {

				pageLoader = new PageLoader(i++);
				pageLoader.addObjectsToPage (pageXML);
				pageLoader.page.transform.SetParent (canvas.transform, false);

			}

			showPageOnWindow (0);
			navPage.text = "1";
		}
	}
}
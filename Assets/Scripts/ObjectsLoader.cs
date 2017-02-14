using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class ObjectsLoader : MonoBehaviour {

		private static List<ObjectXML> allObjects;
		private static InputField inputFileName;
		private static GameObject canvas = GameObject.FindWithTag ("Canvas");
		private static GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
		//private List<GameObject> unityObjects;

		// Use this for initialization
		public static void Load() {

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
			allObjects = XMLDecoder.getData();

			foreach (var objectXML in allObjects) {
				GameObject x = Instantiate(objectXML.instantiateXMLObject()) as GameObject;
				x.name = objectXML.getId();

				x.transform.position = objectXML.getPosition ();
				x.transform.localScale = objectXML.getScale ();
				x.transform.rotation = objectXML.getRotation ();

				x.transform.SetParent(canvas.transform, false);

			}
		}

		public void Start(){
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class ObjectsLoader : MonoBehaviour {

		private List<ObjectXML> allObjects;
		//private List<GameObject> unityObjects;

		// Use this for initialization
		void Start () {
			
			GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
			mainCamera.transform.position = GameObject.FindWithTag ("Canvas").transform.position;

			mainCamera.transform.position += new Vector3 (0.0f, 0.0f, -432.0f); //to adjust visible area with canvas

			XMLDecoder.loadData("Assets/Save_files/Data.xml");
			allObjects = XMLDecoder.getData();

			foreach (var objectXML in allObjects) {
				GameObject x = Instantiate(objectXML.instantiateXMLObject()) as GameObject;
				x.name = objectXML.getId();

				x.transform.position = objectXML.getPosition ();
				x.transform.localScale = objectXML.getScale ();
				x.transform.rotation = objectXML.getRotation ();

				x.transform.SetParent(GameObject.FindWithTag ("Canvas").transform, false);

			}
		}
	}
}

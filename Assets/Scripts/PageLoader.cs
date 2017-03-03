using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace URECA
{
	public class PageLoader {

		public GameObject page = new GameObject ();

		public PageLoader(int num){
			page.name = "Page" + num.ToString ();
		}

		public void addObjectsToPage(PageXML pageXML){



			foreach (ObjectXML objectXML in pageXML.listObjects) {
				addObjectToPage (objectXML);
			}
		}

		public void addObjectToPage(ObjectXML objectXML){

			GameObject gameObjectToAdd = MonoBehaviour.Instantiate(objectXML.instantiateXMLObject ());

			gameObjectToAdd.name = objectXML.getId();

			gameObjectToAdd.transform.position = objectXML.getPosition ();
			gameObjectToAdd.transform.localScale = objectXML.getScale ();
			gameObjectToAdd.transform.rotation = objectXML.getRotation ();

			if (!gameObjectToAdd.GetComponent<MeshCollider> ()) {
				gameObjectToAdd.AddComponent<BoxCollider> ();
				BoxCollider collider = gameObjectToAdd.GetComponent<BoxCollider> ();
				collider.isTrigger = true;

				if (gameObjectToAdd.GetComponent<RectTransform> ()) {
					RectTransform rectTransform = gameObjectToAdd.GetComponent<RectTransform> ();
					collider.size = new Vector3 (rectTransform.rect.width, rectTransform.rect.height, 1);
				} else { // Default size
					collider.size = new Vector3 (50f / objectXML.getScale().x, 50f / objectXML.getScale().y, 1 / objectXML.getScale().z);
				}
			} else {
				gameObjectToAdd.GetComponent<MeshCollider> ().convex = true;
				gameObjectToAdd.GetComponent<MeshCollider> ().isTrigger = true;
			}

			gameObjectToAdd.transform.SetParent (page.transform, false);
		}

	}
}
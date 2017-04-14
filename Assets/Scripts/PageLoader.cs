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
			page.tag = "Page";
		}

		public void addObjectsToPage(PageXML pageXML){



			foreach (ObjectXML objectXML in pageXML.listObjects) {
				addObjectToPage (objectXML);
			}
		}

		public void addObjectToPage(ObjectXML objectXML){

			GameObject gameObjectToAdd = MonoBehaviour.Instantiate(objectXML.instantiateXMLObject ());

			gameObjectToAdd.name = objectXML.getId();

			gameObjectToAdd.transform.position = new Vector3(objectXML.getPositionX (),objectXML.getPositionY (),objectXML.getPositionZ ());
			gameObjectToAdd.transform.localScale = new Vector3(objectXML.getScaleX (),objectXML.getScaleY (),objectXML.getScaleZ ());
			gameObjectToAdd.transform.rotation = Quaternion.Euler(objectXML.getRotationX (),objectXML.getRotationY (),objectXML.getRotationZ ());

//			if (!gameObjectToAdd.GetComponent<MeshCollider> ()) {
//				gameObjectToAdd.AddComponent<BoxCollider> ();
//				BoxCollider collider = gameObjectToAdd.GetComponent<BoxCollider> ();
//				collider.isTrigger = true;
//
//				if (gameObjectToAdd.GetComponent<RectTransform> ()) {
//					RectTransform rectTransform = gameObjectToAdd.GetComponent<RectTransform> ();
//					collider.size = new Vector3 (rectTransform.rect.width, rectTransform.rect.height, 1);
//				} else { // Default size
//					collider.size = new Vector3 (50f / objectXML.getScale().x, 50f / objectXML.getScale().y, 50f / objectXML.getScale().z);
//				}
//			} else {
//				gameObjectToAdd.GetComponent<MeshCollider> ().convex = true;
//				gameObjectToAdd.GetComponent<MeshCollider> ().isTrigger = true;
//			}
//
			if (!gameObjectToAdd.GetComponent<MeshCollider> ()) {

				MeshCollider collider = gameObjectToAdd.AddComponent<MeshCollider> ();

				if (gameObjectToAdd.GetComponent<RectTransform> ()) {
					
//					RectTransform rectTransform = gameObjectToAdd.GetComponent<RectTransform> ();
//
//					temp.transform.position = gameObjectToAdd.transform.position;
//					for (int i=0; i < temp.mesh.vertexCount; i++) {
//						temp.mesh.vertices [i] = new Vector3 (
//							(temp.sharedMesh.vertices [i].x - temp.transform.position.x)
//							* rectTransform.rect.width / gameObjectToAdd.transform.localScale.x + temp.transform.position.x,
//							(temp.sharedMesh.vertices [i].y - temp.transform.position.y)
//							* rectTransform.rect.height / gameObjectToAdd.transform.localScale.y + temp.transform.position.y,
//							temp.sharedMesh.vertices [i].z
//						);
//					}
//					temp.transform.rotation = gameObjectToAdd.transform.rotation;
//
//					collider.sharedMesh = temp.mesh;

					Mesh theMesh = generateBound(gameObjectToAdd);
					collider.sharedMesh = theMesh;

				} else {
					Mesh theMesh = generateBound(gameObjectToAdd);
					collider.sharedMesh = theMesh;
				}
			}

			gameObjectToAdd.GetComponent<MeshCollider> ().convex = true;
			gameObjectToAdd.GetComponent<MeshCollider> ().isTrigger = true;

			if (gameObjectToAdd.tag == "Video") {
				GameObject playButton = GameObject.Instantiate (Resources.Load ("Prefabs/PlayButton") as GameObject);
				playButton.transform.localScale = Vector3.one;
				playButton.transform.SetParent (gameObjectToAdd.transform, true);
				playButton.transform.localPosition = new Vector3(0,1,0);
				playButton.GetComponent<PlayVideo> ().videoToPlay = gameObjectToAdd;
			}

			gameObjectToAdd.AddComponent<MovingTools> ();

			gameObjectToAdd.transform.SetParent (page.transform, false);
		}

		public static Mesh generateBound(GameObject go){
			Mesh theMesh = new Mesh ();
			Bounds bound;
			Quaternion quat = go.transform.rotation;
			Vector3 pos = go.transform.position;
			Vector3 scl = go.transform.localScale;

			go.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			go.transform.position = Vector3.zero;
			go.transform.localScale = Vector3.one;

			if (go.GetComponentsInChildren<MeshFilter> ().Length > 0) {
				MeshFilter[] mf = go.GetComponentsInChildren<MeshFilter> ();

				for (int i = 0; i < mf.Length; i++) {
					Mesh ms = mf [i].mesh;
					Vector3 tr = mf [i].gameObject.transform.localPosition;
					int vc = ms.vertexCount;
					for (int j = 0; j < vc; j++) {
						if (i == 0 && j == 0) {
							bound = new Bounds (Vector3.zero, Vector3.zero);
						} else {
							bound.Encapsulate (tr + ms.vertices [j]);
						}
					}
				}
			} else {
				RectTransform rect = go.GetComponent<RectTransform> ();
				bound = new Bounds (Vector3.zero, new Vector3(
					rect.rect.width,
					rect.rect.height,
					2
				));
			}

			Vector3 offCenter = bound.center;

			go.transform.rotation = quat;
			go.transform.position = pos;
			go.transform.localScale = scl;

			Vector3 topFrontRight = offCenter + Vector3.Scale(bound.extents, new Vector3(1, 1, 1)); 
			Vector3 topFrontLeft = offCenter + Vector3.Scale(bound.extents, new Vector3(-1, 1, 1)); 
			Vector3 topBackLeft = offCenter + Vector3.Scale(bound.extents, new Vector3(-1, 1, -1));
			Vector3 topBackRight = offCenter + Vector3.Scale(bound.extents, new Vector3(1, 1, -1)); 
			Vector3 bottomFrontRight = offCenter + Vector3.Scale(bound.extents, new Vector3(1, -1, 1)); 
			Vector3 bottomFrontLeft = offCenter + Vector3.Scale(bound.extents, new Vector3(-1, -1, 1)); 
			Vector3 bottomBackLeft = offCenter + Vector3.Scale(bound.extents, new Vector3(-1, -1, -1));
			Vector3 bottomBackRight = offCenter + Vector3.Scale(bound.extents, new Vector3(1, -1, -1));
			Vector3[] corners = new Vector3[]{
				topFrontRight, topBackRight, topFrontLeft,
				topBackRight, topBackLeft, topFrontLeft,
				bottomBackLeft, topBackLeft, topBackRight,
				bottomBackRight, bottomBackLeft, topBackRight,
				topFrontRight, bottomBackRight, topBackRight,
				topFrontRight, bottomFrontRight, bottomBackRight,
				topFrontLeft, bottomBackLeft, bottomFrontLeft,
				topFrontLeft, topBackLeft, bottomBackLeft,
				bottomFrontRight, topFrontRight, topFrontLeft,
				bottomFrontRight, topFrontLeft, bottomFrontLeft,
				bottomFrontRight, bottomBackRight, bottomBackLeft,
				bottomFrontRight, bottomBackLeft, bottomFrontLeft
			};

			theMesh.vertices = corners;

			theMesh.triangles = new int[] {
				0,1,2,
				3,4,5,
				6,7,8,
				9,10,11,
				12,13,14,
				15,16,17,
				18,19,20,
				21,22,23,
				24,25,26,
				27,28,29,
				30,31,32,
				33,34,35
			};
				
			return theMesh;
		}
	}
}
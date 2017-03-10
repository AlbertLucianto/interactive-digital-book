using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace URECA
{
	public class ColliderTextFitterSize : MonoBehaviour {
		
		// Update is called once per frame
		void Update () {
			if (GetComponent<MeshCollider> ()) {
				GetComponent<MeshCollider> ().sharedMesh = PageLoader.generateBound (gameObject);
			}
		}
	}
}

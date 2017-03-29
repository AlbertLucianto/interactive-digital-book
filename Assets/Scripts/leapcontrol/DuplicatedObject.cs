using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatedObject : MonoBehaviour {
	private GameObject originObject;

	public GameObject OriginObject {
		get {
			return originObject;
		}
		set {
			originObject = value;
			MeshRenderer[] meshes = originObject.GetComponentsInChildren<MeshRenderer> ();
			foreach (var item in meshes) {
				item.enabled = false;
			}
		}
	}




	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		if(originObject != null)
		{
			MeshRenderer[] meshes = originObject.GetComponentsInChildren<MeshRenderer> ();
			foreach (var item in meshes) {
				item.enabled = true;
			}
		}
	}
}

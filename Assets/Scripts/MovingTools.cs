using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTools : MonoBehaviour {

	private Vector3 curPosition;
	private Vector3 cursor;
	private Vector3 offset;
	private Vector3 screenPoint;
	private Transform thisTransform;
	private GameObject canvas;

	void Start(){
		thisTransform = GetComponent<Transform> ();
		canvas = GameObject.FindWithTag ("Canvas");
	}

	void OnMouseDown(){
		
		curPosition = thisTransform.position;
		cursor = Camera.main.ScreenToWorldPoint(
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z - Camera.main.transform.position.z));
		offset = cursor - curPosition;
	}

	void OnMouseDrag(){
		screenPoint = Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z - Camera.main.transform.position.z));
		thisTransform.position = screenPoint - offset;
	}
}

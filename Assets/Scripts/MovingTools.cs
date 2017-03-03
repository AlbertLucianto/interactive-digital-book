using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTools : MonoBehaviour {

	private Vector3 curPosition;
	private Vector3 cursor;
	private Vector3 offset;
	private Vector3 screenPoint;
	private Transform thisTransform;

	float speed = 250.0f;

	void Start(){
		thisTransform = GetComponent<Transform> ();
	}

	void OnMouseDown(){
		
		curPosition = thisTransform.position;
		cursor = Camera.main.ScreenToWorldPoint(
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z - Camera.main.transform.position.z));
		offset = cursor - curPosition;
	}

	void OnMouseDrag(){

		if(Input.GetKey(KeyCode.LeftAlt)){
			thisTransform.localEulerAngles += new Vector3 (Input.GetAxis ("Mouse Y"), -Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed;
		} else {
		screenPoint = Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z - Camera.main.transform.position.z));
		thisTransform.position = screenPoint - offset;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
	public class MovingTools : MonoBehaviour {

		private Vector3 curPosition;
		private Vector3 cursor;
		private Vector3 offset;
		private Vector3 screenPoint;

		float speed = 250.0f;

		void Start(){
			
		}

		void OnMouseDown(){
			
			curPosition = transform.position;
			cursor = Camera.main.ScreenToWorldPoint(
				new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z - Camera.main.transform.position.z));
			offset = cursor - curPosition;

		}

		void OnMouseDrag(){
			if (!PreviewButton.isInPreview()) {
				if (Input.GetKey (KeyCode.LeftAlt)) {
					transform.localEulerAngles += new Vector3 (Input.GetAxis ("Mouse Y"), -Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed;
				} else {
					screenPoint = Camera.main.ScreenToWorldPoint (
						new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z - Camera.main.transform.position.z));
					transform.position = screenPoint - offset;
				}

				if (GetComponent<BoundBoxes_BoundBox> ()) {
					gameObject.GetComponent<BoundBoxes_BoundBox> ().init ();
				}
			}
		}

		void OnMouseUp(){
			
		}
	}
}
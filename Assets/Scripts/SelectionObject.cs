using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
	public class SelectionObject : MonoBehaviour {

		public static GameObject selected;
		RaycastHit hitInfo = new RaycastHit();
		
		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonDown (0)) {

				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

				if (hit) {

					if (hitInfo.transform.gameObject.tag == "ItrMan") {
//						Debug.Log ("Do nothing");

					} else {
						if (selected &&
						    selected != hitInfo.transform.gameObject &&
						    selected.GetComponent<BoundBoxes_BoundBox> ()) {

							Destroy (selected.GetComponent<BoundBoxes_BoundBox> ());
						}

						StartCoroutine (selectAfterDestroy ());
					}

					Debug.Log ("Hit LMB " + hitInfo.transform.gameObject.name);

				} else {
					
					if (selected && selected.GetComponent<BoundBoxes_BoundBox> ()) {
						Destroy (selected.GetComponent<BoundBoxes_BoundBox> ());
					}

					selected = null;
				}
			}

			if (Input.GetMouseButtonDown (1)) {

				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

				if (hit) {
					Debug.Log ("Hit RMB " + hitInfo.transform.gameObject.name);
					selected = hitInfo.transform.gameObject;
				} else {
					selected = null;
				}
			}
		}

		IEnumerator selectAfterDestroy(){

			if (selected && selected.GetComponent<BoundBoxes_BoundBox> ())
				yield return new WaitForSeconds (0);

			selected = hitInfo.transform.gameObject;

			if (!selected.GetComponent<BoundBoxes_BoundBox> ()) {
				selected.AddComponent<BoundBoxes_BoundBox> ();
			}
		}

	}
}

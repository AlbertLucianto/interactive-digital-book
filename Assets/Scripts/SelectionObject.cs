using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
	public class SelectionObject : MonoBehaviour {
		
		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonDown (0)) {

				RaycastHit hitInfo = new RaycastHit();

				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

				if (hit) 
				{
					Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace URECA
{
	public class ColliderTextFitterSize : MonoBehaviour {

		RectTransform textRect;

		// Use this for initialization
		void Start () {
			textRect = GetComponent<RectTransform> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (GetComponent<BoxCollider> ()) {
				GetComponent<BoxCollider> ().size = new Vector3 (textRect.rect.width, textRect.rect.height, 1);
			}
		}
	}
}

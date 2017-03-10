using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
	public class Interaction_ObjectRotatorBySound : MonoBehaviour {
		public float speed = 10f;
		private MicControl micControl;
		// Use this for initialization
		void Start () {
			micControl = GameObject.Find ("MicController").GetComponent<MicControl>();
		}

		// Update is called once per frame
		void Update () {
			if (PreviewButton.isInPreview()) {
				float loudness = micControl.loudness;
				if (loudness < 0.5)
					loudness = 0;
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * loudness * speed, transform.eulerAngles.z);
			}
		}
	}
}
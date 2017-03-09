using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
	public class Interaction_ObjectRotatorBySound : MonoBehaviour {
		public float speed = 100f;
		private MicControl micControl;
		// Use this for initialization
		void Start () {
			micControl = GameObject.Find ("MicController").GetComponent<MicControl>();
		}

		// Update is called once per frame
		void Update () {
			float loudness = micControl.loudness;
			if (loudness < 0.5)
				loudness = 0;
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * loudness*100, transform.eulerAngles.z);
		}
	}
}
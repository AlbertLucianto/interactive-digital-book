using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
	public class Interaction_ObjectRotatorBySound : InteractionBase {
		public float speed = 10f;
		private MicControl micControl;
		private float blowDuration = 0f;
		private List<float> timestamp = new List<float>();
		private List<float> loudnessOfEachFrame = new List<float>();
		// Use this for initialization
		void Start () {
			micControl = GameObject.Find ("MicController").GetComponent<MicControl>();
		}

		private float getPast1SecondsLoudness()
		{
			for(int i = timestamp.Count-1; i>0; i--)
			{
				if(Time.time - timestamp[i] > 1f)
				{
					timestamp.RemoveAt (i);
					loudnessOfEachFrame.RemoveAt (i);
				}
			}
			float sum = 0f;
			for(int j =0; j <loudnessOfEachFrame.Count; j++)
			{
				sum += loudnessOfEachFrame [j];
			}
			return sum / loudnessOfEachFrame.Count;

		}

		// Update is called once per frame
		void Update () {
			if (PreviewButton.isInPreview()) {
				float loudness = micControl.loudness;
				timestamp.Add (Time.time);
				loudnessOfEachFrame.Add (loudness);
				float averageLoudness = getPast1SecondsLoudness ();
				if (averageLoudness< 2f)
				{
					return;
				}
				Debug.Log ("rotating, current loudness is:" + averageLoudness);
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * loudness * speed, transform.eulerAngles.z);
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class VoiceGestureController : MonoBehaviour {
	Frame currentFrame;
	public float indexFingersThreshold = 80f;
	private VoiceRecognition _regconition;
	// Use this for initialization
	private bool _started = false;
	void Start () {
		_regconition = this.GetComponent<VoiceRecognition> ();
		if(_regconition == null)
		{
			_regconition = this.gameObject.AddComponent<VoiceRecognition> ();
		}
	}

	private int availableHandNum(HandList hands)
	{
		int count = 0;
		for (int i = 0; i < hands.Count; i++) {
			if(hands[i].IsValid)
			{
				count++;
			}
		}
		return count;
	}

	private bool onlyIndexFingerExtend(Hand h)
	{
		bool indexExtended = false;
		bool otherExtented = false;
		FingerList fingers = h.Fingers;
		for(int i = 0; i < fingers.Count; i++)
		{
			if (fingers [i].Type == Finger.FingerType.TYPE_INDEX)
			{
				if (fingers [i].IsExtended)
					indexExtended = true;
			}
			else
			{
				if (fingers [i].IsExtended)
					otherExtented = true;
			}
		}
		return indexExtended && (!otherExtented);
	}

	private Finger getIndexFinger(Hand h)
	{
		for (int i = 0; i < h.Fingers.Count; i++) {
			if(h.Fingers[i].Type == Finger.FingerType.TYPE_INDEX)
			{
				return h.Fingers [i];
			}
		}
		return null;

	}
	
	// Update is called once per frame
	void Update () {
		currentFrame = HandController.Main.GetFrame ();
		HandList hands = this.currentFrame.Hands;
		if(availableHandNum(hands) == 2)
		{
			if(onlyIndexFingerExtend(hands[0]) && onlyIndexFingerExtend(hands[1]))
			{
				float twoIndexFingersDistance = getIndexFinger (hands [0]).TipPosition.DistanceTo (getIndexFinger (hands [1]).TipPosition);
				Debug.Log ("distance:" + twoIndexFingersDistance);
				Debug.Log (twoIndexFingersDistance);
				if(twoIndexFingersDistance < indexFingersThreshold)
				{
					Debug.Log ("start recording");
					if(!_started)
					{
						_regconition.StartListening ();
						_started = true;
					}
				}
				else
				{
					if(_started)
					{
						_regconition.StopListening ();
						_started = false;
					}

					Debug.Log ("stop recording");
				}
			}
		}
		else
		{
			if(_started)
			{
				_regconition.StopListening ();
				_started = false;
			}
		}
	}
}

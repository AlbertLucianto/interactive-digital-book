﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using URECA;

public class PreviewPageLeapControl : MonoBehaviour {
	public HandController hc;
	private HandModel hm;
	public PageNav pageNav;
	private const float SWIPE_MIN_INTERVAL = 0.5f;
	private float _interval = 0f;
	Frame currentFrame;
	Frame lastFrame = null;
	Frame thisFrame = null;
	// Use this for initialization
	void Start () {
		hc = HandController.Main;
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPECIRCLE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPESWIPE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		//hc.GetLeapController().Config.SetFloat ("Gesture.Swipe.MinLength", 200.0f);
		//hc.GetLeapController().Config.SetFloat("Gesture.Swipe.MinVelocity", 750f);
	}

	void OnEnable()
	{
		EventDispatcher.AddEventListener (SystemEvent.VoiceResult, voiceHandler);
	}

	void OnDisable()
	{
		EventDispatcher.RemoveEventListener (SystemEvent.VoiceResult, voiceHandler);
	}



	private void voiceHandler(EventObject obj)
	{
		string result = obj.param as string;
		Debug.Log (result);
		if (result.Contains("next")|| result.Contains("right"))
		{
			pageNav.incPageNumAndLoad ();

		}
		else if(result.Contains("previous") || result.Contains("left"))
		{
			pageNav.decPageNumAndLoad ();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		_interval += Time.deltaTime;
		currentFrame = hc.GetFrame();
		GestureList gestures = this.currentFrame.Gestures();
		foreach (Gesture g in gestures)
		{
			if (g.Type == Gesture.GestureType.TYPESWIPE && LeapModeController.instance ().mode == LeapMode.NoRotatingOjbectInScene)
			{
				if (_interval < SWIPE_MIN_INTERVAL)
					return;
				_interval = 0;
				SwipeGesture swipe = new SwipeGesture (g);
				if(swipe.Direction.x<0)
				{
					pageNav.incPageNumAndLoad();
					Debug.Log ("left gesture, go to next page");
				}
				if(swipe.Direction.x > 0)
				{
					pageNav.decPageNumAndLoad ();
					Debug.Log ("right gesture, go to previous page");
				}
			}
		}

		foreach (var h in hc.GetFrame().Hands)
		{
			if (h.IsRight)
			{
				
				foreach (var f in h.Fingers)
				{
					if (f.Type == Finger.FingerType.TYPE_INDEX)
					{
						
					}
				}

			}
			if (h.IsLeft)
			{
				
			}

		}

	}
}
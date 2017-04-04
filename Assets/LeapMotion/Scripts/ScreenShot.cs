using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenShot : MonoBehaviour {
	void OnMouseDown() {
		Debug.Log ("screenshot");
		Application.CaptureScreenshot("Screenshot.png");
	}
}
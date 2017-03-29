using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace URECA
{
	public class PreviewButton : MonoBehaviour {

		private static bool inPreview;

		private Button previewButton;
		private GameObject previewCanvas;
		private GameObject canvas;
		private GameObject workingWindow;

		private float newScale;
		private Vector3 workingPosition;
		private Vector3 buttonPosition;

		// Use this for initialization
		void Start () {
			previewCanvas = GameObject.FindWithTag ("PreviewCanvas");
			canvas = GameObject.FindWithTag ("Canvas");
			workingPosition = canvas.transform.localPosition;
			workingWindow = GameObject.FindWithTag ("Window");

			previewCanvas.SetActive (false);
			inPreview = false;

			newScale = previewCanvas.GetComponent<RectTransform> ().rect.width /
				canvas.GetComponent<RectTransform> ().rect.width;
//				previewCanvas.GetComponent<RectTransform> ().rect.height/
//				canvas.GetComponent<RectTransform> ().rect.height);

			previewButton = GetComponent<Button> ();
			previewButton.onClick.AddListener (showHidePreview);
		}

		void showHidePreview(){

			if (!inPreview) {
				inPreview = true;
				previewCanvas.SetActive (true);

				gameObject.transform.SetParent (previewCanvas.transform, false);
				previewButton.GetComponentInChildren<Text> ().text = "Exit";
				gameObject.transform.localScale = new Vector3 (1, 1, 1);

				canvas.transform.SetParent (previewCanvas.transform, false);
				canvas.transform.localPosition = new Vector3 (0, 0, 0);
				canvas.transform.localScale = new Vector3 (newScale, newScale, newScale);

				gameObject.transform.SetAsLastSibling ();
				LeapModeController.instance ().mode = LeapMode.RotatingOjbect;

				workingWindow.SetActive (false);
			} else {
				inPreview = false;
				workingWindow.SetActive (true);

				gameObject.transform.SetParent (workingWindow.transform, false);
				previewButton.GetComponentInChildren<Text> ().text = "Preview";

				canvas.transform.SetParent (workingWindow.transform, false);
				canvas.transform.localPosition = workingPosition;
				canvas.transform.localScale = new Vector3 (1,1,1);

				gameObject.transform.SetAsLastSibling ();

				previewCanvas.SetActive (false);
			}
		}

		public static bool isInPreview(){
			return inPreview;
		}
	}
}

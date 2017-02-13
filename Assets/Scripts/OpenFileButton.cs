using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class OpenFileButton : MonoBehaviour {

		private Button openButton;

		// Use this for initialization
		void Start () {
			openButton = GetComponent<Button> ();

			openButton.onClick.AddListener(ObjectsLoader.Load);
		}
	
	}
}

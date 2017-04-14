using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class SaveButton : MonoBehaviour {

		private Button saveButton;

		// Use this for initialization
		void Start () {
			saveButton = GetComponent<Button> ();
			saveButton.onClick.AddListener(delegate{ObjectSaver.saveObjects(); XMLEncoder.saveToXML();});	
		}

	}
}

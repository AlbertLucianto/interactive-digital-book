using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace URECA
{
	public class InteractionManager : MonoBehaviour {

		private Button button;
		// Use this for initialization
		void Start () {
			button = GetComponent<Button> ();

			button.onClick.AddListener (addInteractionScript);
		}

		void addInteractionScript () {
//			var scriptName =  GameObject.GetComponent(Type.GetType(button.GetComponentInChildren<Text> ().text));
			if(SelectionObject.selected)
				SelectionObject.selected.AddComponent<Interaction_ObjectRotatorBySound>();
		}
	}
}

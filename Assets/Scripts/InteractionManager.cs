using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class InteractionManager : MonoBehaviour {

		private Button button;
		// Use this for initialization
		void Start () {
			button = GetComponent<Button> ();

			button.onClick.AddListener (addInteractionScript);
		}
		
		// Update is called once per frame
		void addInteractionScript () {
			SelectionObject.selected.AddComponent<Interaction_ObjectRotatorBySound>();
		}
	}
}

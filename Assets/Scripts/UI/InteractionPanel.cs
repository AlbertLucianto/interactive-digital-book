using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using URECA;

public class InteractionPanel : MonoBehaviour {
	private Button addBtn;
	private Button removeBtn;
	private Button closeBtn;
	private InteractionComboBox combobox;
	// Use this for initialization
	void Start () {
		addBtn = transform.Find ("addBtn").GetComponent<Button> ();
		removeBtn = transform.Find ("removeBtn").GetComponent<Button> ();
		closeBtn = transform.Find ("closeBtn").GetComponent<Button> ();
		combobox = transform.Find ("Combobox/Combobox").GetComponent<InteractionComboBox> ();
		addBtn.onClick.AddListener (addInteractionListener);
		removeBtn.onClick.AddListener (removeInteractionListener);
		closeBtn.onClick.AddListener (close);
	}

	private void addInteractionListener()
	{
		if(SelectionObject.selected)
		{
			Debug.Log (combobox.currentValue);
			switch (combobox.currentValue) {
			case "Interaction_ObjectRotatorBySound":
				if (SelectionObject.selected.GetComponent<Interaction_ObjectRotatorBySound> () == null)
					SelectionObject.selected.AddComponent<Interaction_ObjectRotatorBySound> ();
				break;
			default:
				break;
			}				
		}
	}



	private void removeInteractionListener()
	{

	}

	private void close()
	{
		this.gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAndDisappear : MonoBehaviour {
	public GameObject controlObject;
	private Button btn;
	// Use this for initialization
	void Start () {
		btn = this.GetComponent<Button> ();
		btn.onClick.AddListener(showOrHide);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showOrHide()
	{
		if(controlObject)
		{
			if (controlObject.activeSelf)
				controlObject.SetActive (false);
			else
				controlObject.SetActive (true);
		}
	}
}

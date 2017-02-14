using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

	private Button playButton;

	// Use this for initialization
	void Start () {
		playButton = GetComponent<Button> ();

		playButton.onClick.AddListener(Play);
	}
	
	// Update is called once per frame
	void Play () {
		GameObject a = GameObject.FindWithTag ("Video");
		Renderer b = a.GetComponent<Renderer> ();
		MovieTexture c = (MovieTexture)b.material.mainTexture;
		AudioSource d = a.GetComponent<AudioSource> ();

		d.Play ();
		c.Play ();
	}
}

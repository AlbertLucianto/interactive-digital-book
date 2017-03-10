using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

	private Button playButton;
	public GameObject videoToPlay;
	// Use this for initialization
	void Start () {
		playButton = GetComponent<Button> ();

		playButton.onClick.AddListener(Play);
	}
	
	// Update is called once per frame
	void Play () {
		Renderer b = videoToPlay.GetComponent<Renderer> ();
		MovieTexture c = (MovieTexture)b.material.mainTexture;
		AudioSource d = videoToPlay.GetComponent<AudioSource> ();
//		Debug.Log (videoToPlay.name);

		if (c.isReadyToPlay && d.clip.loadState.Equals(AudioDataLoadState.Loaded)) {
			if (!c.isPlaying) {
				d.Play ();
				c.Play ();
				GetComponentInChildren<Text> ().text = "Pause";
			} else {
				d.Pause ();
				c.Pause ();
				GetComponentInChildren<Text> ().text = "Play";
			}
		}

//		if (c.isReadyToPlay) {
//			c.Play ();
//		}
	}
}

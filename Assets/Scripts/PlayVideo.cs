using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

	private Button playButton;
	public GameObject videoToPlay;
	Renderer renderer;
	MovieTexture movie;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		playButton = GetComponent<Button> ();

		playButton.onClick.AddListener(playHandler);
		EventDispatcher.AddEventListener (SystemEvent.VoiceResult, voiceHandler);
		renderer = videoToPlay.GetComponent<Renderer> ();
		movie = (MovieTexture)renderer.material.mainTexture;
		audio = videoToPlay.GetComponent<AudioSource> ();
	}

	private void voiceHandler(EventObject obj)
	{
		string result = obj.param as string;
		if(result.Contains("play video"))
		{
			play ();
		}
		if(result.Contains("pause")&& result.Contains("video"))
		{
			pause ();
		}
	}

	void play()
	{
		audio.Play ();
		movie.Play ();
		GetComponentInChildren<Text> ().text = "Pause";
	}
	
	// Update is called once per frame
	void playHandler () {
		
//		Debug.Log (videoToPlay.name);

		if (movie.isReadyToPlay && audio.clip.loadState.Equals(AudioDataLoadState.Loaded)) {
			if (!movie.isPlaying) {
				play ();
			} else {
				pause ();
			}
		}
	}

	void pause()
	{
		audio.Pause ();
		movie.Pause ();
		GetComponentInChildren<Text> ().text = "Play";
	}

	void OnDestory()
	{
		EventDispatcher.RemoveEventListener (SystemEvent.VoiceResult, voiceHandler);
	}
}

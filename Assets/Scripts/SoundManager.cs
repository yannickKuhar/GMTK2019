using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SoundManager : MonoBehaviour {
 
	// Holds the single instance of the SoundManager that 
	// you can access from any script
	public static SoundManager Instance = null;
 
	// All sound effects in the game
	// All are public so you can set them in the Inspector
	public AudioClip walk;
	public AudioClip run;
	public AudioClip jump;
	public AudioClip cave;
 
	// Refers to the audio source added to the SoundManager
	// to play sound effects
	public AudioSource soundEffectAudio;
 
	// Use this for initialization
	void Start()
	{
		// This is a singleton that makes sure you only
		// ever have one Sound Manager
		// If there is any other Sound Manager created destroy it
		if (Instance == null)
		{
			Instance = this;
		} else if (Instance != this)
		{
			Destroy (gameObject);
		}
 
		AudioSource theSource = GetComponent<AudioSource> ();
		soundEffectAudio = theSource;
 
	}

	public void Stop()
	{
		soundEffectAudio.Stop();
	}

	public void Play(AudioClip clip)
	{
		if (soundEffectAudio.isPlaying == false)
		{
			soundEffectAudio.clip = clip;
			soundEffectAudio.Play();
		}
	}
	
	public void StopPlay(AudioClip clip)
	{
		if (soundEffectAudio.isPlaying == false)
		{
			soundEffectAudio.clip = clip;
			soundEffectAudio.Play();
		}
		else if (clip )
		{
			soundEffectAudio.Stop();
			soundEffectAudio.clip = clip;
			soundEffectAudio.Play();
		}
	}
	public void StopContinuousPlay(AudioClip clip)
	{
		if (soundEffectAudio.isPlaying == false)
		{
			soundEffectAudio.clip = clip;
			soundEffectAudio.Play();
		}
		else if (clip )
		{
			if (!soundEffectAudio.clip.ToString().Equals(run.ToString()))
			{
				soundEffectAudio.Stop();
				soundEffectAudio.clip = clip;
				soundEffectAudio.Play();
			}
		}
	}
 
	// Other GameObjects can call this to play sounds
	public void PlayOneShot(AudioClip clip)
	{

		if (soundEffectAudio.isPlaying == false)
		{
			soundEffectAudio.PlayOneShot(clip);
		}
	}
}

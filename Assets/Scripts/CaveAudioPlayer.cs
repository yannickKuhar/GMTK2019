using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveAudioPlayer : MonoBehaviour
{
	
	public AudioSource soundEffectAudio;

	void Awake()
	{
		AudioSource theSource = GetComponent<AudioSource> ();
		soundEffectAudio = theSource;
	}

    void Update()
    {
		if (soundEffectAudio.isPlaying == false)
		{
			soundEffectAudio.Play();
		}
    }
}

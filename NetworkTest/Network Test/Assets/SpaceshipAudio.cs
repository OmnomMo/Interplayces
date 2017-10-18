using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAudio : MonoBehaviour {

    public AudioClip thrustStart;
    public AudioClip thrustLoop;
    public AudioClip shield;

    public AudioSource thrustStart_s;
    public AudioSource thrustLoop_s;
    public AudioSource shield_s;

	// Use this for initialization
	void Start () {
        thrustStart_s = AddAudioAudioSource(thrustStart, false, false, 0.1f, 1.5f);
        thrustLoop_s = AddAudioAudioSource(thrustLoop, true, false, 0.7f);
        shield_s = AddAudioAudioSource(shield, false, false, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public AudioSource AddAudioAudioSource(AudioClip clip, bool loop, bool playAwake, float vol, float pitch = 1)
    {
     AudioSource newAudio = this.gameObject.AddComponent<AudioSource>();
     newAudio.clip = clip; 
     newAudio.loop = loop;
        newAudio.pitch = pitch;
     newAudio.playOnAwake = playAwake;
     newAudio.volume = vol; 
     return newAudio; 
 }
}

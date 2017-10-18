using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    public bool startedPlaying = false;

    public void startPlaying()
    {
        startedPlaying = true;
        GetComponent<AudioSource>().Play();
    }

	// Update is called once per frame
	void Update () {
		if (startedPlaying && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
	}
}

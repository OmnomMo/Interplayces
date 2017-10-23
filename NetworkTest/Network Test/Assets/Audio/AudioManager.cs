using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {

    public bool followSpaceship;

    public AudioSource musicSource;

    public AudioClip[] music;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnEnterScene;
    }

    void OnDisable()
    {
        Debug.Log("WTF2");
        SceneManager.sceneLoaded -= OnEnterScene;
    }


    public void SwitchSong(int n)
    {
        musicSource.Stop();
        musicSource.clip = music[n];
        musicSource.Play();
    }

	// Use this for initialization
	void Start () {
        instance = this;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (followSpaceship)
        {
            transform.position = SpaceshipGameplay.Instance.gameObject.transform.position;
            //transform.rotation = SpaceshipGameplay.Instance.gameObject.transform.rotation;
        } else
        {
            transform.position = Camera.main.gameObject.transform.position;
            //transform.rotation = SpaceshipGameplay.Instance.gameObject.transform.rotation;
        }
	}

    void OnEnterScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded!");

        followSpaceship = false;
        StartCoroutine(DelayedGetSpaceShip());

    }


    public IEnumerator DelayedGetSpaceShip()
    {

        SwitchSong((int)Random.Range(0, 2f));
        yield return null;
        if (SpaceshipGameplay.Instance != null)
        {
            
            Debug.Log("Spaceship found. Attaching audiomanager");
            followSpaceship = true;
        }
    }
}

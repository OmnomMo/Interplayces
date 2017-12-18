using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {

    public bool followSpaceship;

    public AudioSource musicSource;

    //public AudioClip[] music;

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
        //Debug.Log("WTF2");
        SceneManager.sceneLoaded -= OnEnterScene;
    }


    //public void SwitchSong(int n)
    //{
    //    musicSource.Stop();
    //    musicSource.clip = music[n];
    //    musicSource.Play();
    //}

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadedObjectManager.Instance.AddPersistenObject(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (SpaceshipGameplay.Instance != null)
        {
            if (followSpaceship)
            {
                transform.position = SpaceshipGameplay.Instance.gameObject.transform.position;
                //transform.rotation = SpaceshipGameplay.Instance.gameObject.transform.rotation;
            }
            else
            {
                transform.position = Camera.main.gameObject.transform.position;
                //transform.rotation = SpaceshipGameplay.Instance.gameObject.transform.rotation;
            }
        }
	}

    void OnEnterScene(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Scene loaded!");

        AudioClip newMusic = GetLevelMusic();
        if (newMusic != GetComponent<AudioSource>().clip)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = newMusic;
            GetComponent<AudioSource>().Play();
        }

        followSpaceship = false;
        StartCoroutine(DelayedGetSpaceShip());

    }

    public AudioClip GetLevelMusic()
    {
        return Camera.main.GetComponent<LevelMusic>().levelMusic;
    }

    public IEnumerator DelayedGetSpaceShip()
    {

        //SwitchSong((int)Random.Range(0, 2f));
        yield return null;
        if (SpaceshipGameplay.Instance != null)
        {
            
            Debug.Log("Spaceship found. Attaching audiomanager");
            followSpaceship = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamManager : MonoBehaviour {

    public    WebCamTexture tex;



    private static WebcamManager instance;
    public static WebcamManager Instance
    {
        get { return instance; }
    }



    void Awake()
    {
        instance = this;
        Object.DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        tex = new WebCamTexture(WebCamTexture.devices[0].name);

        tex.Play();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamManager : MonoBehaviour {

    public    WebCamTexture tex;

    public bool hasWebcam;


    private static WebcamManager instance;
    public static WebcamManager Instance
    {
        get { return instance; }
    }



    void Awake()
    {
        instance = this;
        Object.DontDestroyOnLoad(gameObject);

        try
        {

            tex = new WebCamTexture(WebCamTexture.devices[0].name);

            hasWebcam = true;

            tex.Play();

        } catch (System.Exception e)
        {
            Debug.Log("No Webcam found");
            hasWebcam = false;
        }
    
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

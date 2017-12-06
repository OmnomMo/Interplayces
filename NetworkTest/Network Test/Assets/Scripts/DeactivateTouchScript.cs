using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTouchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
       GameObject.Find("TouchScript").GetComponent<TouchScript.InputSources.StandardInput>().enabled = false;
        GameObject.Find("TouchScript").SetActive(false);
        Camera.main.GetComponent<TouchScript.Layers.CameraLayer>().enabled = false;
       // GameObject.Find("EventSystem").GetComponent<Base>
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("TouchScript").GetComponent<TouchScript.InputSources.StandardInput>().enabled = false;
        GameObject.Find("TouchScript").SetActive(false);
        Camera.main.GetComponent<TouchScript.Layers.CameraLayer>().enabled = false;
        //GameObject.Find("TouchScript").GetComponent<TouchScript.InputSources.StandardInput>().enabled = false;
        //GameObject.Find("TouchScript").SetActive(false);
        //GameObject.Find("TouchScript").GetComponent<TouchScript.InputSources.StandardInput>().enabled = false;
        //GameObject.Find("TouchScript").SetActive(false);
        //Camera.main.GetComponent<TouchScript.Layers.CameraLayer>().enabled = false;
    }
}

using UnityEngine;
using System.Collections;


//Changes Camera Depending on Player Type
public class CameraBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeCameraToNavigator()
    {
        Camera.main.orthographicSize = 35;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplay : MonoBehaviour {


    public bool multiDisplayActive;

	// Use this for initialization
	void Start () {
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

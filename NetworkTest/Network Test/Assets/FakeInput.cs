using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeInput : MonoBehaviour {

    float timeLastInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Sessionmanagement.Instance != null && NetworkActions.Instance != null)
        {
            if (Time.time - timeLastInput > 0.1f)
            {
                NetworkActions.Instance.CmdRegisterInput();
                timeLastInput = Time.time;
            }
        }

	}
}

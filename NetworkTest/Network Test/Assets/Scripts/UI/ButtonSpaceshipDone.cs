using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpaceshipDone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void SpaceshipDone()
    {
        EndBuilding.Instance.EndPhase();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

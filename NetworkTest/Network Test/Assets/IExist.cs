using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IExist : MonoBehaviour {

    private void Awake()
    {
        Debug.Log(gameObject.ToString() + " has been created");
    }

    private void OnDestroy()
    {
        Debug.Log(gameObject.ToString() + " has been destroyed... :( ");
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.ToString() + " has been enabled... :) ");
    }


    private void OnDisable()
    {
        Debug.Log(gameObject.ToString() + " has been disabled... :( ");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject.ToString() + ": 'I exist!'");
	}
}

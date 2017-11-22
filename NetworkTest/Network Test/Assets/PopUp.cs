using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour {

    public GameObject model;

    public float startSize;
    public float currentSize;
    public float endSize;

    public bool growing;
    public float growSpeed;

    bool started = false;

	// Use this for initialization
	void Start () {
		
	}
	
    public void StartPopup()
    {
        if (!started)
        {
            model.GetComponent<MeshRenderer>().enabled = true;
            model.transform.localScale = new Vector3(startSize, startSize, startSize);
            currentSize = startSize;
            growing = true;
            started = true;
        }
    }

	// Update is called once per frame
	void Update () {

        if (growing)
        {
            currentSize = currentSize * (1 + (growSpeed * Time.deltaTime));

            if (currentSize > endSize)
            {
                currentSize = endSize;
                growing = false;
            }

            model.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnd : MonoBehaviour {

    ParticleSystem p;


	// Use this for initialization
	void Start () {
        p = GetComponent<ParticleSystem>();	
	}
	
	// Update is called once per frame
	void Update () {
		
        if (p)
        {
            if (!p.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }

	}
}

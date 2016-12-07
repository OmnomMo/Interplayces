using UnityEngine;
using System.Collections;

public class PointToTarget : MonoBehaviour {

    Transform targetPlanet;
    bool pointerActive;

	// Use this for initialization
	void Start () {
        pointerActive = true;
	}
	
    public void setTargetPlanet(Transform target)
    {
        targetPlanet = target;
        
    }

    public void unsetTargetPlanet()
    {
        targetPlanet = null;
    }

	// Update is called once per frame
	void Update () {
	
        if (targetPlanet == null)
        {

            if (pointerActive)
            {
                foreach (MeshRenderer r in transform.GetComponentsInChildren<MeshRenderer>())
                {
                    r.enabled = false;
                }
                pointerActive = false;
            }
        } else
        {
            if (!pointerActive)
            {
                foreach (MeshRenderer r in transform.GetComponentsInChildren<MeshRenderer>())
                {
                    r.enabled = true;
                }
                pointerActive = true;
            }

            transform.LookAt(targetPlanet);

        }

	}
}

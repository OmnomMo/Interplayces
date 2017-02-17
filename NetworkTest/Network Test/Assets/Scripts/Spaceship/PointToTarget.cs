using UnityEngine;
using System.Collections;

public class PointToTarget : MonoBehaviour {

    public Transform targetPlanet;
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
	
        if (targetPlanet == null || Vector3.Magnitude(new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(targetPlanet.transform.position.x, 0 , targetPlanet.transform.position.z)) < 50)
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
           // transform.localEulerAngles = new Vector3(0, transform.localRotation.y, 0);

        }

	}
}

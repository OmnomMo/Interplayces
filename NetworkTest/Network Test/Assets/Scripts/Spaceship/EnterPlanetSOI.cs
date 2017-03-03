using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlanetSOI : MonoBehaviour {

    public bool playerInSOI;
    public GameObject planet;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlanetSOI"))
        {

            if (planet != null)
            {
                planet.GetComponent<PlanetInfo>().HideInfo();
                planet.GetComponent<PlanetInfo>().playerStay = false;
            }

            playerInSOI = true;
            planet = other.gameObject.transform.parent.gameObject;
            planet.GetComponent<PlanetInfo>().ShowInfo();
            planet.GetComponent<PlanetInfo>().playerStay = true;
            Score.Instance.AddScanToPoints(planet);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlanetSOI"))
        {
            playerInSOI = false;

            if (planet != null)
            {
                planet.GetComponent<PlanetInfo>().HideInfo();
                planet.GetComponent<PlanetInfo>().playerStay = false;
            }
        }
    }

    //In case player leaves SOI but is still in other SOI
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlanetSOI"))
        {
            if (!playerInSOI)
            {
                playerInSOI = true;
                planet = other.gameObject.transform.parent.gameObject;
                planet.GetComponent<PlanetInfo>().ShowInfo();
                planet.GetComponent<PlanetInfo>().playerStay = true;
                Score.Instance.AddScanToPoints(planet);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

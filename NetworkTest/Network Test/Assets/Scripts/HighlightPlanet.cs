using UnityEngine;
using System.Collections;

public class HighlightPlanet : MonoBehaviour {

    public GameObject parentPlanet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {

      //if (GameState.Instance.isPlayerNavigator())
        {

            Debug.Log("Click on Planet");
            PlanetNavigation.Instance.RequestHighlight(parentPlanet);
        }
    }



    public void SetHighlight()
    {
        GameObject.Find("HighlightSphere").transform.position = transform.position;
        GameObject.Find("HighlightSphere").SetActive(true);
        //GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
    }

    public void UnsetHighlight()
    {
        GameObject.Find("HighlightSphere").SetActive(false);
        //GetComponent<Renderer>().material.shader = Shader.Find("Earth");
    }
}

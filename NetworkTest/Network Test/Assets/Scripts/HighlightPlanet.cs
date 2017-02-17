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
        GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
    }

    public void UnsetHighlight()
    {
        GetComponent<Renderer>().material.shader = Shader.Find("Earth");
    }
}

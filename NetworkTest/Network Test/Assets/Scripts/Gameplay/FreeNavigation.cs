using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;



public class FreeNavigation : NetworkBehaviour {

    public bool navigationActive;
    public GameObject highlightSphere;

    public Vector3 targetPoint;
    public bool targetPointActive;


    private static FreeNavigation instance;
    public static FreeNavigation Instance { get { return instance; } }


    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}

    public void OnMouseDown()
    {

    }

    // Update is called once per frame
    void Update () {

        //Change Scale depending on camera distance;
        float scale = Camera.main.transform.position.y / 100;
        highlightSphere.transform.localScale = new Vector3(scale, scale, scale);

       if (GameState.Instance.isPlayerNavigator())
        {




            if (Input.GetMouseButtonUp(0))
            {

                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {

                    StartCoroutine(GetDelayedMousePos(Input.mousePosition));
                }

            }
        }
    }


    //Get mouse position and convert it to world position.
    //Tell network to highlight position
    public IEnumerator GetDelayedMousePos(Vector2 posMouse) 
    {
        yield return null;

        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {


            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - 10));
            NetworkActions.Instance.CmdHighlightMousePosition(pos.x, pos.y, pos.z);
        }

    }

    //Set highlight sphere position to target position, set sphere visible
    [ClientRpc]
    public void RpcSetTargetPoint(float posX, float posY, float posZ)
    {

        Vector3 p = new Vector3(posX, posY, posZ);
        
        Debug.Log("FreeNavigation: OnMouseDown\n" + p);
        
        targetPoint = p;
        targetPointActive = true;
        highlightSphere.transform.position = p;

        if (GameState.Instance.isPlayerNavigator())
        {
            highlightSphere.GetComponent<MeshRenderer>().enabled = true;
            

        }

        if (GameState.Instance.isPlayerCaptain())
        {
            //highlightSphere.transform.position = p;
            // pointer.GetComponent<PointToTarget>().setTargetPlanet(planets[activePlanet].transform);
        }
    }
}

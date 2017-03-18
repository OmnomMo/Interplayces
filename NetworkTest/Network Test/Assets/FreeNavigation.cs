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

            //bool touch = false;
            //// Check if there is a touch
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
                
            //    // Check if finger is over a UI element
            //    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //    {
            //        Debug.Log("Touch!");
            //        Vector2 fingerPos = Input.GetTouch(0).position;
            //        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(fingerPos.x, fingerPos.y, Camera.main.transform.position.y - 10));
            //        NetworkActions.Instance.CmdHighlightMousePosition(pos.x, pos.y, pos.z);
            //        touch = true;
            //    }
            //}


            if (Input.GetMouseButtonDown(0))
            {

                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {

                    StartCoroutine(GetDelayedMousePos(Input.mousePosition));
                    //Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - 10));
                    //NetworkActions.Instance.CmdHighlightMousePosition(pos.x, pos.y, pos.z);
                }

            }
        }
    }

    public IEnumerator GetDelayedMousePos(Vector2 posMouse) 
    {
        yield return new WaitForSeconds(0.1f);

        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {


            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - 10));
            NetworkActions.Instance.CmdHighlightMousePosition(pos.x, pos.y, pos.z);
        }

    }

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

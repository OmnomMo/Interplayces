using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragAround : NetworkBehaviour {

    [SyncVar]
    public bool dragged;
    public GameObject partContainer;
    public GameObject partStack;

	// Use this for initialization
	void Start () {
        dragged = false;
	}
	
    void OnMouseDown()
    {
        if (GameState.Instance.isPlayerCaptain())
        {
            NetworkActions.Instance.CmdDragSphere(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Collision Test. Is cube on field?");
        if (other.gameObject.tag == "PlayingField")
        {
            partContainer.GetComponent<FollowSphere>().isOnField = true;
            //Debug.Log("Cube is on field!");
        }
    }
    [ClientRpc]
    public void RpcStartDrag()
    {
       // Debug.Log("Start Dragging " + gameObject.ToString());
        dragged = true;
    }
    [ClientRpc]
    public void RpcStopDrag()
    {
        dragged = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayingField")
        {
            partContainer.GetComponent<FollowSphere>().isOnField = false;
            PlayingGrid.Instance.RemovePiece(partContainer);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            if (dragged)
            {
               // Debug.Log("True");
                Vector3 mPos = Input.mousePosition;
                Vector3 newPos = Camera.main.ScreenToWorldPoint(mPos);
                newPos.z = transform.position.z;
                transform.position = newPos;
            } 
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Stop Dragging");
            NetworkActions.Instance.CmdStopDragSphere(gameObject);
            transform.position = new Vector3(partContainer.transform.position.x, partContainer.transform.position.y, transform.position.z);

            if (!partContainer.GetComponent<FollowSphere>().isOnField)
            {
                GameObject.Destroy(partContainer);

                partStack.GetComponent<ShipPartStack>().ReturnPart();

                GameObject.Destroy(this.gameObject);

            }
        }
	}
}

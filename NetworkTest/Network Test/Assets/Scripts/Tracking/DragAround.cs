using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragAround : NetworkBehaviour {

    [SyncVar]
    public bool dragged;
    public GameObject partContainer;

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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayingField")
        {
            partContainer.GetComponent<FollowSphere>().isOnField = true;
        }
    }
    [ClientRpc]
    public void RpcStartDrag()
    {
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
                Vector3 mPos = Input.mousePosition;
                Vector3 newPos = Camera.main.ScreenToWorldPoint(mPos);
                newPos.z = transform.position.z;
                transform.position = newPos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            NetworkActions.Instance.CmdStopDragSphere(gameObject);
        }
	}
}

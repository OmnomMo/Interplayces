using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAround : MonoBehaviour {

    public bool dragged;
    public GameObject partContainer;

	// Use this for initialization
	void Start () {
        dragged = false;
	}
	
    void OnMouseDown()
    {
        dragged = true;

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayingField")
        {
            partContainer.GetComponent<FollowSphere>().isOnField = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayingField")
        {
            partContainer.GetComponent<FollowSphere>().isOnField = false;
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
            dragged = false;
        }
	}
}

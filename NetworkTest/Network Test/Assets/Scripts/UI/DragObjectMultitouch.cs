using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectMultitouch : MonoBehaviour {

    public Transform followingObject;

    bool stoppedDrag;

	// Use this for initialization
	void Start () {
		
	}


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayingField")
        {
            followingObject.gameObject.GetComponent<FollowSphere>().isOnField = false;
          

            //Debug.Log("Trying to remove " + followingObject.GetComponentInChildren<ShipPart>().ToString() );
            //Debug.Log("Cube is on field!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayingField")
        {
            followingObject.gameObject.GetComponent<FollowSphere>().isOnField = true;
            //Debug.Log(followingObject.GetComponentInChildren<ShipPart>().ToString() + " is on field!");
        }
    }


    // Update is called once per frame
    void Update () {

         //Debug.Log(GetComponent<TouchScript.Gestures.TransformGesture>().State);


        if (GetComponent<TouchScript.Gestures.TransformGesture>().State == TouchScript.Gestures.Gesture.GestureState.Changed)
        {
            stoppedDrag = false;
            
        }


            if (GetComponent<TouchScript.Gestures.TransformGesture>().State == TouchScript.Gestures.Gesture.GestureState.Possible && !stoppedDrag) 
        {
            stoppedDrag = true;
            //Debug.Log("StopDrag!");
            //gest = GetComponent<TouchScript.Gestures.TransformGesture>();
            transform.position = new Vector3(followingObject.transform.position.x, followingObject.transform.position.y, transform.position.z);

            NetworkActions.Instance.CmdRegisterInput();

        }

	}
}

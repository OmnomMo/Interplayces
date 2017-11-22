using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour {

    public GameObject sphere;
    public GameObject playingField;
    public bool isOnField;

    public int UID;

    bool followDelayed;

	// Use this for initialization
	void Start () {
        isOnField = false;
	}



    // Update is called once per frame
    void Update () {

       // if (sphere.GetComponent<DragAround>().dragged)
        {

            transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, transform.position.z);
        }


        //If the object is on the playing field and is being dragged at the moment
        if ( /*sphere.GetComponent<DragAround>().dragged &&*/ isOnField && sphere.GetComponent<TouchScript.Gestures.TransformGesture>().State == TouchScript.Gestures.Gesture.GestureState.Changed)
        {
            PlayingGrid.Instance.IncludePieceV02(gameObject);
            followDelayed = true;
        } else
        {

            //Update Field one frame after object is dragged as well
            //Follow one frmae longer
            if (followDelayed)
            {
                PlayingGrid.Instance.IncludePieceV02(gameObject);
                followDelayed = false;

                if (!isOnField)
                {
                    bool removed = PlayingGrid.Instance.RemovePiece(gameObject);
                }
            } else
            {
                
               // Debug.Log("Stop Follow delayed");
            }

            
            //PlayingGrid.Instance.RemovePiece(gameObject);
        }

        


    }
}

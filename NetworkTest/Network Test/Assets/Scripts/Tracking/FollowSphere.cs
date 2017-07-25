using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour {

    public GameObject sphere;
    public GameObject playingField;
    public GameObject playingGrid;
    public bool isOnField;

    bool followDelayed;

	// Use this for initialization
	void Start () {
        isOnField = false;
	}



    // Update is called once per frame
    void Update () {

       // if (sphere.GetComponent<DragAround>().dragged)
        {

            transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, -2);// transform.position.z);
        }


        if ( /*sphere.GetComponent<DragAround>().dragged &&*/ isOnField)// && sphere.GetComponent<TouchScript.Gestures.TransformGesture>().State == TouchScript.Gestures.Gesture.GestureState.Changed)
        {
            playingGrid.GetComponent<PlayingGrid>().IncludePieceV02(gameObject);
            followDelayed = true;
        } else
        {
            //Follow one frmae longer
            if (followDelayed)
            {
                //playingGrid.GetComponent<PlayingGrid>().IncludePieceV02(gameObject);

                NetworkActions.Instance.CmdSetPartTypes(transform.GetComponentInChildren<ShipPart>().GetPosX(), transform.GetComponentInChildren<ShipPart>().GetPosY(), 4);
                followDelayed = false;

                if (!isOnField)
                {
                    bool removed = playingGrid.GetComponent<PlayingGrid>().RemovePieceNonDestructive(gameObject);
                }
            } else
            {
                
               // Debug.Log("Stop Follow delayed");
            }
            //PlayingGrid.Instance.RemovePiece(gameObject);
        }

        


    }
}

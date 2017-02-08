using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour {

    public GameObject sphere;
    public GameObject playingField;
    public bool isOnField;

	// Use this for initialization
	void Start () {
        isOnField = false;
	}
	

   
	// Update is called once per frame
	void Update () {

        if (sphere.GetComponent<DragAround>().dragged)
        {

            transform.position = new Vector3(sphere.transform.position.x, sphere.transform.position.y, transform.position.z);
        }


        if ( sphere.GetComponent<DragAround>().dragged && isOnField )
        {
            PlayingGrid.Instance.IncludePiece(gameObject);
        }

        


    }
}

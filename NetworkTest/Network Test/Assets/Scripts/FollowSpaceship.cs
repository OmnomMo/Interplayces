using UnityEngine;
using System.Collections;

public class FollowSpaceship : MonoBehaviour {

    public Transform spaceship;
    Vector3 camPos;

    public float camHeight;

    public bool followRotation;

	// Use this for initialization
	void Start () {

        camPos = new Vector3();

	}

    // Update is called once per frame
    void Update()
    {

        if (followRotation)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, spaceship.transform.eulerAngles.y, transform.eulerAngles.z); 
        }

        camPos.x = spaceship.position.x;


        if (GameState.Instance != null)
        {
            //if (GameState.Instance.isPlayerCaptain())
            //{
            //    camPos.y = 100;
            //}
            //if (GameState.Instance.isPlayerNavigator())
            //{
                camPos.y = camHeight;
           // }
        }

        camPos.z = spaceship.position.z;

        transform.position = camPos;
    }
}

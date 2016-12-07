using UnityEngine;
using System.Collections;

public class FollowSpaceship : MonoBehaviour {

    public Transform spaceship;
    Vector3 camPos;

	// Use this for initialization
	void Start () {

        camPos = new Vector3();

	}

    // Update is called once per frame
    void Update()
    {

        camPos.x = spaceship.position.x;
        camPos.y = 20;
        camPos.z = spaceship.position.z;

        transform.position = camPos;
    }
}

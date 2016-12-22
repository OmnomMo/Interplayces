using UnityEngine;
using System.Collections;

using UnityEngine.Networking;
using System;

public class SpaceshipMovement : NetworkBehaviour {

    
    public float thrust;
    bool isAccelerating;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {

        if (NetworkPlayer.Instance != null)
        {
            if (NetworkPlayer.Instance.isPlayerCaptain())
            {

                //Debug.Log("CaptainMyCaptain");

                var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
                //isAccelerating = Input.GetAxis("Vertical") > 0;

                transform.Rotate(0, x, 0);
                //transform.Translate(0, 0, z);


                if (Input.GetKey(KeyCode.Space))
                {
                    isAccelerating = true;
                }
                else
                {
                    isAccelerating = false;
                }
            }
        }
    }

    [ClientRpc]
    internal void RpcSetThrust(int energy)
    {
        thrust = energy;
    }

    void FixedUpdate()
    {
        if (isAccelerating)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
        }
    }
}

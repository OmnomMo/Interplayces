using UnityEngine;
using System.Collections;

using UnityEngine.Networking;
using System;

public class SpaceshipMovement : NetworkBehaviour {

    
    public float thrust;
    [Range(0.0f, 10.0f)]
    public float thrustMultiplier;
    bool isAccelerating;
    public GameObject rotationTarget;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {

        if (NetworkPlayer.Instance != null)
        {

        


            if (NetworkPlayer.Instance.isPlayerCaptain())
            {

                ////Debug.Log("CaptainMyCaptain");

                //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
                ////isAccelerating = Input.GetAxis("Vertical") > 0;

                //transform.Rotate(0, x, 0);
                ////transform.Translate(0, 0, z);

                bool noHorizontalInput = Input.GetAxis("HorizontalKeyboard") < 0.1f && Input.GetAxis("HorizontalKeyboard") > -0.1f;

                

                bool noVerticalInput = Input.GetAxis("VerticalKeyboard") < 0.1f && Input.GetAxis("VerticalKeyboard") > -0.1f;

                Debug.Log("" + noHorizontalInput + noVerticalInput);

                if (!(noHorizontalInput && noVerticalInput))
                {
                    Debug.Log("Rotation changing maybe!");
                    rotationTarget.transform.position = new Vector3(Camera.main.transform.position.x + 5 * Input.GetAxis("HorizontalKeyboard"), transform.position.y, Camera.main.transform.position.z + 5 * Input.GetAxis("VerticalKeyboard"));
                    transform.LookAt(rotationTarget.transform);
                }


                if (Input.GetButton("Accelerate"))
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

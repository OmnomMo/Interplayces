using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour {

    public float speed;
    public float maxSpeed;

    public float rotationSpeed;


    public bool turningRight;
    public bool turningLeft;

    public bool accelerating;

    public GameObject thrustL;
    public GameObject thrustLTurning;
    public GameObject thrustR;
    public GameObject thrustRTurning;

    // Use this for initialization
    void Start () {
        thrustL.GetComponent<ParticleSystem>().enableEmission = false;
        thrustLTurning.GetComponent<ParticleSystem>().enableEmission = false;
        thrustR.GetComponent<ParticleSystem>().enableEmission = false;
        thrustRTurning.GetComponent<ParticleSystem>().enableEmission = false;

    }
	
    public void StartTurningL()
    {
        turningLeft = true;
        thrustLTurning.GetComponent<ParticleSystem>().enableEmission = true;
    }

    public void StopTurningL()
    {
        turningLeft = false;
        thrustLTurning.GetComponent<ParticleSystem>().enableEmission = false;
    }

    public void StartTurningR()
    {
        turningRight = true;
        thrustRTurning.GetComponent<ParticleSystem>().enableEmission = true;
    }

    public void StopTurningR()
    {
        turningRight = false;
        thrustRTurning.GetComponent<ParticleSystem>().enableEmission = false;
    }


    public void StartAccelerating()
    {
        accelerating = true;
        thrustL.GetComponent<ParticleSystem>().enableEmission = true;
        thrustR.GetComponent<ParticleSystem>().enableEmission = true;
    }

    public void StopAccelerating()
    {
        accelerating = false;
        thrustL.GetComponent<ParticleSystem>().enableEmission = false;
        thrustR.GetComponent<ParticleSystem>().enableEmission = false;
    }
    // Update is called once per frame
    void Update () {
		


	}

    void FixedUpdate()
    {
        if (accelerating && Vector3.Magnitude(GetComponent<Rigidbody>().velocity) < maxSpeed)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        }

        if (turningLeft)
        {
            GetComponent<Rigidbody>().AddTorque(0,  -1 *rotationSpeed, 0);
        }

        if (turningRight)
        {
            GetComponent<Rigidbody>().AddTorque(0, rotationSpeed, 0);
        }
    }
}

﻿using UnityEngine;
using System.Collections;

using UnityEngine.Networking;
using System;

public class SpaceshipMovement : NetworkBehaviour {

    
    //public float thrust;
    //[Range(0.0f, 10.0f)]
    public float thrustMultiplier;
     float boostMultiplier;
    public float boostFactor;
    public float DEBUGThrustBonus;

    public float maxVelocity;

    public GameObject[] thrusters;
    public GameObject[] allParts;

    public bool isAccelerating;
    
    bool isBraking;

    public float dragNormal;
    public float dragBraking;

    public GameObject rotationTarget;
    public GameObject rotationTarget2;
    public float rotationSpeed;
    public float drainPerFrame = 1f;

    public bool rotateVersion02;

    // Use this for initialization
    void Start () {
        allParts = new GameObject[0];
        StartCoroutine(collectThrusters());
	}

    public IEnumerator collectThrusters()
    {
        yield return null;
        thrusters = GetComponent<SpaceshipParts>().allThrusters;
    }

    // Update is called once per frame
    void Update() {

        if (NetworkPlayer.Instance != null)
        {



            //Only captain may control spaceship
            if (GameState.Instance.isPlayerCaptain())
            {

                //if hololens is connected, send position info to hololens

                if (GameState.Instance.holoLensConnected)
                {

                    Message m = new Message();

                    string[] coordinates = new string[2];
                    coordinates[0] = transform.position.x.ToString();
                    coordinates[1] = transform.position.y.ToString();

                    m.commandID = (int)NetworkCommands.CmdSetPosition;
                    m.parameters = coordinates;
                    TCPSocketServer.Instance.Send(m);
                }

            

                float deadZone = 0.4f;

                bool noHorizontalInput = Input.GetAxis("HorizontalKeyboard") < deadZone && Input.GetAxis("HorizontalKeyboard") > deadZone * -1;
                bool noVerticalInput = Input.GetAxis("VerticalKeyboard") < deadZone && Input.GetAxis("VerticalKeyboard") > deadZone * -1;


                if (!rotateVersion02)
                {
                    //If there is any keyboard input at all, place rotationtarget in direction of input, look at direction rotation target is transformed to
                    if (!(noHorizontalInput && noVerticalInput))
                    {
             
                        //rotates spaceship towards joystick direction


                        float targetAngle = Mathf.Atan(Input.GetAxis("VerticalKeyboard") / Input.GetAxis("HorizontalKeyboard")) * Mathf.Rad2Deg * -1;


                        

                        if (Input.GetAxis("HorizontalKeyboard") >= 0 ) // && Input.GetAxis("VerticalKeyboard") >= 0)
                        {
                            targetAngle = targetAngle  + 90;
                        }

                        if (Input.GetAxis("HorizontalKeyboard") < 0)
                        {
                            targetAngle = targetAngle - 90;
                        }



                        Vector3 targetRotation = new Vector3(0, targetAngle, 0);
                        iTween.RotateTo(gameObject, targetRotation, 1/rotationSpeed);




                        //Debug.Log("Rotate towards: " + targetAngle);
                        ////Vector3 targetRotation = new Vector3(Input.GetAxis("HorizontalKeyboard"),0,Input.GetAxis("VerticalKeyboard"));
                        //Debug.Log("Rotate towards: " + Input.GetAxis("HorizontalKeyboard") + "/" + Input.GetAxis("VerticalKeyboard"));

                        if (targetAngle < 0)
                        {
                            targetAngle = targetAngle + 360;
                        }


                        //Accelerates when direction pointed at is same as target direction

                        //Debug.Log("TargetAngle= " + targetAngle + " rotation = " + transform.eulerAngles.y);
                        if (Mathf.Abs(targetAngle - transform.eulerAngles.y) < 10)
                        {
                            isAccelerating = true;
                        }else
                        {
                            isAccelerating = false;
                        }


                    } else
                    {
                        isAccelerating = false;
                    }
                } else
                {
                    

                    rotationTarget2.transform.Rotate(new Vector3(0, rotationSpeed * Input.GetAxis("HorizontalKeyboard") * Time.deltaTime, 0));
                    transform.LookAt(rotationTarget2.transform.GetChild(0));

                }


                if (Input.GetButton("Accelerate"))
                {
                    // isAccelerating = true;
                    boostMultiplier = boostFactor;
                }
                else
                {
                    //isAccelerating = false;
                    boostMultiplier = 1;
                }

                if (Input.GetButton("Brake"))
                {
                    isBraking = true;
                }
                else
                {
                    isBraking = false;
                }
            }
        }
    }

    void FixedUpdate()
    {

        rotationTarget2.transform.position = transform.position;

        //Debug Acceleration without Navigator

        if (isAccelerating)
        {
            if (Vector3.Magnitude(GetComponent<Rigidbody>().velocity) < maxVelocity)
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * DEBUGThrustBonus * thrustMultiplier * boostMultiplier);
            }
        }

        //if (SpaceshipGameplay.Instance.energy > 0)
        if (isAccelerating && SpaceshipGameplay.Instance.energy > 0)
        {


            if (Vector3.Magnitude(GetComponent<Rigidbody>().velocity) < maxVelocity)
            {

                GetComponent<Rigidbody>().AddForce(transform.forward * SpaceshipGameplay.Instance.thrustPower * thrusters.Length * thrustMultiplier * boostMultiplier);

                SpaceshipGameplay.Instance.DrainEnergy(SpaceshipGameplay.Instance.thrustPower * thrusters.Length * drainPerFrame);

                foreach (GameObject thruster in thrusters)
                {
                    thruster.GetComponent<SpaceShipPart_Thruster>().Fire();
                }
            }
        } else
        {
            foreach (GameObject thruster in thrusters)
            {
                thruster.GetComponent<SpaceShipPart_Thruster>().StopFire();
            }
        }

        if (isBraking)
        {
            GetComponent<Rigidbody>().drag = dragBraking;
        } else
        {
            GetComponent<Rigidbody>().drag = dragNormal;
        }
    }

    //private GameObject[] GetThrusters()
    //{
    //    allParts = GetComponent<SpaceshipParts>().GetActiveParts();

    //    Debug.Log("N Parts: " + allParts.Length);

    //    int nThrusters = 0;

    //    foreach (GameObject part in allParts)
    //    {
    //        if (part.GetComponent<SpaceShipPart_Thruster>() != null)
    //        {
    //            nThrusters++;
    //        }
    //    }

    //    GameObject[] thrusters = new GameObject[nThrusters];
    //    int i = 0;

    //    foreach (GameObject part in allParts)
    //    {
    //        if (part.GetComponent<SpaceShipPart_Thruster>() != null)
    //        {
    //            thrusters[i++] = part;
    //        }
    //    }

    //    return thrusters;

    //}

    
}

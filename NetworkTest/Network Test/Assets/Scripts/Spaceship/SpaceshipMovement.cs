using UnityEngine;
using System.Collections;

using UnityEngine.Networking;
using System;

public class SpaceshipMovement : NetworkBehaviour {

    
    public float thrust;
    [Range(0.0f, 10.0f)]
    public float thrustMultiplier;

    public GameObject[] thrusters;
    public GameObject[] allParts;

    bool isAccelerating;
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

               

                float deadZone = 0.4f;

                bool noHorizontalInput = Input.GetAxis("HorizontalKeyboard") < deadZone && Input.GetAxis("HorizontalKeyboard") > deadZone * -1;
                bool noVerticalInput = Input.GetAxis("VerticalKeyboard") < deadZone && Input.GetAxis("VerticalKeyboard") > deadZone * -1;


                if (!rotateVersion02)
                {
                    //If there is any keyboard input at all, place rotationtarget in direction of input, look at direction rotation target is transformed to
                    if (!(noHorizontalInput && noVerticalInput))
                    {
                        // Debug.Log("Rotation changing maybe!");
                        rotationTarget.transform.position = new Vector3(Camera.main.transform.position.x + 5 * Input.GetAxis("HorizontalKeyboard"), transform.position.y, Camera.main.transform.position.z + 5 * Input.GetAxis("VerticalKeyboard"));
                        transform.LookAt(rotationTarget.transform);
                    }
                } else
                {
                    

                    rotationTarget2.transform.Rotate(new Vector3(0, rotationSpeed * Input.GetAxis("HorizontalKeyboard"), 0));
                    transform.LookAt(rotationTarget2.transform.GetChild(0));

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

        rotationTarget2.transform.position = transform.position;

        if (isAccelerating && SpaceshipGameplay.Instance.energy > 0)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * thrust * thrusters.Length);

            SpaceshipGameplay.Instance.DrainEnergy(thrust * thrusters.Length * drainPerFrame);

            foreach (GameObject thruster in thrusters)
            {
                thruster.GetComponent<SpaceShipPart_Thruster>().Fire();
            } 
        } else
        {
            foreach (GameObject thruster in thrusters)
            {
                thruster.GetComponent<SpaceShipPart_Thruster>().StopFire();
            }
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

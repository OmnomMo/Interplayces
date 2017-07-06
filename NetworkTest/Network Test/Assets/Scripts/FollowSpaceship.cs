using UnityEngine;
using System.Collections;


//Camera Script
//Follows spaceship position every frame

public class FollowSpaceship : MonoBehaviour {

    public Transform spaceship;
    Vector3 camPos;

    public float camHeight;

    public bool followRotation;
    public float timeLastSent;

    // Use this for initialization
    void Start () {

        GetComponent<Camera>().farClipPlane = 200000;

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

           // Debug.Log(Camera.main.transform.position.y.ToString());

            if (GameState.Instance.holoLensConnected)
            {
                if (Time.time - timeLastSent > 0.1f)
                {


                    timeLastSent = Time.time;
                    Message m = new Message();

                    string[] cameraY = new string[1];




                    cameraY[0] = Camera.main.transform.position.y.ToString();
                    
                    m.commandID = (int)NetworkCommands.CmdCameraY;
                    m.parameters = cameraY;
                    TCPSocketServer.Instance.Send(m);
                }
            }
        }

        camPos.z = spaceship.position.z;

        transform.position = camPos;
    }
}

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

       


        if (GameState.Instance != null)
        {
            camPos.y = camHeight;
            if (GameState.Instance.isPlayerCaptain())
            {
                camPos.x = spaceship.position.x;
            } else
            {

                float uiWidth;

                Vector3 rightWorldPoint;
                Vector3 centerWorldPoint;


                rightWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(1920, 540, camHeight));
                centerWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(1333, 540, camHeight));

                uiWidth = Vector3.Distance(rightWorldPoint, centerWorldPoint);

                camPos.x = spaceship.position.x + uiWidth/2;
            }

            //if (GameState.Instance.isPlayerCaptain())
            //{
            //    camPos.y = 100;
            //}
            //if (GameState.Instance.isPlayerNavigator())
            //{
           
            // }


            camPos.z = spaceship.position.z;

            transform.position = camPos;
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

       
    }
}

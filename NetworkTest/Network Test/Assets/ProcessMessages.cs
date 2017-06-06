using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMessages : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        //if connected to server



        List<Message> messages = TCPSocketServer.Instance.GetMessages;
        //Debug.Log ("Client not null, are there any messages? " + (messages != null && messages.Count >0));
        if (messages != null && messages.Count > 0)
        {
            while (messages.Count > 0)
            {
                Message receivedMsg = messages[0];
                messages.RemoveAt(0);
                Debug.Log("MESSAGE RECEIVED = " + receivedMsg.commandID);
                int command = (int)receivedMsg.commandID;
                switch (command)
                {
                    case (int)NetworkCommands.ReqSetScan:
                        {
                            float value = -1;
                            float.TryParse(receivedMsg.parameters[0], out value);
                            Debug.Log("Scan Value: " + value);
                            SpaceshipGameplay.Instance.scanPower = value;
                            break;
                        }
                    case (int)NetworkCommands.ReqSetThrust:
                        {
                            float value = -1;
                            float.TryParse(receivedMsg.parameters[0], out value);
                            Debug.Log("Thrust Value: " + value);
                            SpaceshipGameplay.Instance.thrustPower = value;
                            break;
                        }

                    case (int)NetworkCommands.ReqSetShield:
                        {
                            float value = -1;
                            float.TryParse(receivedMsg.parameters[0], out value);
                            Debug.Log("Shield Value: " + value);
                            SpaceshipGameplay.Instance.shieldPower = value;
                            break;
                        }
                }

            }
        }
    }

}

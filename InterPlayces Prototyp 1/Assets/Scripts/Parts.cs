using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
FUNCTIONS:

    GetRoboWeight()                                  Returns Weigth of Robot depending on currennt number of parts
    CountActiveRoboParts()
    AddRoboPart(GameObject Robopart)                 Adds new Robopart to Robot
    RemoveRobopart(GameObject Robopart)
    ActivateAllParts()                               Activates mechanics (springs, motors,...) on all Roboparts depending on their type
    ResetAllParts()                                  Calls Reset function on each Robopart
    FreezeParts()                                    Parts stop being affected by Physics
    UnFreezeParts()                                  Parts are being affected by Physics again
    ConnectAllPartsRectangular()                     Creates and instances rectangular connection pieces between parts
    ConnectAllParts()                                Creates and instances connection pieces between parts
    ResetConnections()                               Removes all connection pieces and calls ConnectAllParts() Again
    DeleteAllConnections()                           
    TranslateRobot()                                 Translates Robot depending on lowest part, so it stands on the ground


    */

public class Parts : MonoBehaviour {

    public float weightFactor;

    public bool rectangularConnections;

    private static Parts instance;
    public static Parts Instance
    {
        get { return instance; }
    }

    public LinkedList<GameObject> roboparts;
    public GameObject connection;
    public GameObject connection3d;

    public bool _3DConnections;
    
    public GameObject connectionCube;

    void Awake()
    {
        instance = this;

        //DontDestroyOnLoad(transform.gameObject);
    }

    public void Swap2D3D()
    {

        Debug.Log("SwapStuff");


        LinkedList<GameObject> tempParts = new LinkedList<GameObject>();

        foreach (GameObject part in roboparts)
        {
            //roboparts.Remove(part);

            GameObject newPart = GameObject.Instantiate(part.GetComponent<Robopart>().correspondingPart);
            newPart.transform.parent = part.transform.parent;
            newPart.transform.localPosition = part.transform.localPosition;
            newPart.transform.localRotation = part.transform.localRotation;

            if (part.GetComponent<Robopart>().Connection != null)
            {
                //GameObject connection = part.GetComponent<Robopart>().Connection;

                //GameObject newConnection;

                //if (!_3DConnections)
                //{
                //    newConnection = GameObject.Instantiate(connection.GetComponent<Connection>().correspondingConnection);
                //}
                //else
                //{
                //    newConnection = GameObject.Instantiate(connection3d.GetComponent<Connection>().correspondingConnection);
                //}

                //newConnection.transform.parent = connection.transform.parent;
                //newConnection.transform.localPosition = connection.transform.localPosition;

                //newConnection.transform.Translate(new Vector3(0, 0, 1), Space.Self);

                //newConnection.transform.localRotation = connection.transform.localRotation;
                //newConnection.transform.localScale = connection.transform.localScale;


                //part.GetComponent<Robopart>().Connection = newConnection;

                //connection.SetActive(false);
                // Debug.Log("Is Cative? " + connection.activeInHierarchy);

                GameObject.DestroyImmediate(part.GetComponent<Robopart>().Connection, true);

            }

            tempParts.AddLast(newPart);

            if (!part.activeInHierarchy)
            {
                newPart.SetActive(false);
            }

            GameObject.Destroy(part);
        }

        roboparts = new LinkedList<GameObject>();

        foreach (GameObject newPart in tempParts)
        {
            //tempParts.Remove(newPart);
            roboparts.AddLast(newPart);
        }
    }

    public float GetRoboWeight()
    {
        return Mathf.Pow(CountActiveRoboparts(), 1f / 3f) * weightFactor;
    }

    public int CountActiveRoboparts()
    {
        int n = 0;
        foreach (GameObject go in roboparts)
        {
            if (go.activeInHierarchy)
            {
                n++;
            }
        }

        return n;
    }

	// Use this for initialization
	void Start () {
        roboparts = new LinkedList<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.Instance.IsInSetup())
        {
           // Parts.Instance.ResetConnections();
        }
    }

    public void AddRobopart(GameObject robopart)
    {
        //Debug.Log(roboparts.ToString());
        roboparts.AddLast(robopart);
    }

    //Removes specified robopart 
    public void RemoveRobopart(GameObject robopart)
    {

        roboparts.Remove(robopart);
    }

    //Activeates all different Roboparts
    //TODO: Call single function in parent class
    public void ActivateAllParts()
    {
        foreach (GameObject p in roboparts)
        {
            if (p.GetComponent<Robopart>().type == 1 && p.activeInHierarchy == true)
            {
                p.GetComponent<HingePart>().UnHinge();
            }

            if (p.GetComponent<Robopart>().type == 2 && p.activeInHierarchy == true)
            {

                p.GetComponent<SpringPart>().UnSpring();
              
            }


            if (p.GetComponent<Robopart>().type == 3 && p.activeInHierarchy == true)
            {

                p.GetComponent<PropellerPart>().isTurning = true;

            }

            if (p.GetComponent<Robopart>().type == 4 && p.activeInHierarchy == true)
            {
                //Debug.Log("FIREIREREIERERERER");
                p.GetComponent<JetPart>().StartEngines();

            }
        }
    }

    public void ActivateBoost()
    {
        foreach (GameObject p in roboparts)
        {
           

            if (p.GetComponent<Robopart>().type == 4 && p.activeInHierarchy == true)
            {
                //Debug.Log("Boost");
                p.GetComponent<JetPart>().ResetJet();
                p.GetComponent<JetPart>().StartBoost();

            }
        }
    }


    public void ResetAllParts()
    {
        foreach (GameObject p in roboparts)
        {
            if (p.GetComponent<Robopart>().type == 1)
            {
                p.GetComponent<HingePart>().ReHinge();
            }

            if (p.GetComponent<Robopart>().type == 2)
            {

                p.GetComponent<SpringPart>().ReSpring();

            }


            if (p.GetComponent<Robopart>().type == 3)
            {

                p.GetComponent<PropellerPart>().isTurning = false;

            }

            if (p.GetComponent<Robopart>().type == 4)
            {

                p.GetComponent<JetPart>().ResetJet();

            }
        }
    }

    //Make all roboparts kinematic
    public void FreezeParts()
    {

        foreach (HingePart hp in GetComponentsInChildren<HingePart>())
        {
            hp.ConnectHinges();
        }

        foreach (SpringPart sp in GetComponentsInChildren<SpringPart>())
        {
            sp.ConnectHinges();
        }

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {


            rb.isKinematic = true;
        }
    }


    public void UnFreezeParts()
    {
        //Reset Physics Joints
        foreach (HingePart hp in GetComponentsInChildren<HingePart>())
        {
            hp.ConnectHinges();
        }

        foreach (SpringPart sp in GetComponentsInChildren<SpringPart>())
        {
            sp.ConnectHinges();
        }


        //if (rb.gameObject.GetComponent<Robopart>().type == 1)
        //{
        //    
        //}

        //if (rb.gameObject.GetComponent<Robopart>().type == 2)
        //{
        //    rb.gameObject.GetComponent<SpringPart>().ConnectHinges();
        //}

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
    

            rb.isKinematic = false;

            Debug.Log("Unfreeze!");
        }
    }


    public void ConnectAllPartsRectangular()
    {
        foreach (GameObject p in roboparts)
        {
            //Get nearest neighbour

            if (p.activeSelf)
            {


                float minDistance = float.MaxValue;
                GameObject connectedPart = null;

                //Get Center of Robot.
                int nParts = 0;
                float centerX = 0;
                float centerY = 0;

                foreach (GameObject p1 in roboparts)
                {
                    if (p.activeSelf)
                    {
                        nParts += 1;
                        centerX += p1.transform.position.x;
                        centerY += p1.transform.position.y;
                    }

                }

                centerX = centerX / nParts;
                centerY = centerY / nParts;

                foreach (GameObject p2 in roboparts)
                {
                    if (!p.Equals(p2) && p2.activeSelf)
                    {
                        float distance;


                        //Calculate Distance
                        distance = Mathf.Sqrt(Mathf.Pow(p2.transform.position.x - p.transform.position.x, 2) + Mathf.Pow(p2.transform.position.y - p.transform.position.y, 2));




                        //if distance is smallest AND connected part is closer to center than connecting part
                        if (distance < minDistance && Mathf.Sqrt(Mathf.Pow(p2.transform.position.x - centerX, 2) + Mathf.Pow(p2.transform.position.y - centerY, 2)) < Mathf.Sqrt(Mathf.Pow(p.transform.position.x - centerX, 2) + Mathf.Pow(p.transform.position.y - centerY, 2)))
                        {
                            minDistance = distance;
                            connectedPart = p2;
                        }

                    }
                }

                //If there are two, use the one that is closer to the center, if they are the same distance, use both
                //If its exists connect te two objects

                if (minDistance < float.MaxValue)
                {
                    GameObject newConnectionHorizontal = GameObject.Instantiate(connection);
                    newConnectionHorizontal.GetComponent<Connection>().SetupConnection(p.transform.position.x, p.transform.position.y, p.transform.position.x, connectedPart.transform.position.y);
                    newConnectionHorizontal.transform.position = new Vector3(p.transform.position.x, newConnectionHorizontal.transform.position.y, p.transform.position.z);

                    GameObject newConnectionVertical = GameObject.Instantiate(connection);
                    newConnectionVertical.GetComponent<Connection>().SetupConnection(p.transform.position.x, connectedPart.transform.position.y, connectedPart.transform.position.x, connectedPart.transform.position.y);
                    newConnectionVertical.transform.position = new Vector3(newConnectionVertical.transform.position.x, connectedPart.transform.position.y, p.transform.position.z);

                    if (newConnectionHorizontal.GetComponent<Connection>().length <= 0.5f)
                    {
                        GameObject.Destroy(newConnectionHorizontal);
                    }
                    else
                    {

                        p.GetComponent<Robopart>().Connection = newConnectionHorizontal;
                    }

                    if (newConnectionVertical.GetComponent<Connection>().length <= 0.5f)
                    {
                        GameObject.Destroy(newConnectionVertical);
                    } else
                    {
                        p.GetComponent<Robopart>().Connection2 = newConnectionVertical;
                    }

                    if (p.GetComponent<Robopart>().Connection != null && p.GetComponent<Robopart>().Connection2 != null)
                    {
                        GameObject newConnectionCube = GameObject.Instantiate(connectionCube);
                        newConnectionCube.transform.position = new Vector3(p.transform.position.x, connectedPart.transform.position.y, p.transform.position.z);

                        newConnectionCube.transform.parent = Parts.instance.gameObject.transform;
                        p.GetComponent<Robopart>().ConnectionCube = newConnectionCube;
                    }

                }



                //Parent connection under Robot Prefab

                // Maybe: Store connection in LinkedList -> Use it later? (Break connections, Check which parts a Part is connected with)
            }
        }
    }
    //Create connections between all active parts.
    public void ConnectAllParts()
    {
        foreach (GameObject p in roboparts)
        {
            //Get nearest neighbour

            if (p.activeSelf)
            {
            

                float minDistance = float.MaxValue;
                GameObject connectedPart = null;

                //Get Center of Robot.
                int nParts = 0;
                float centerX = 0;
                float centerY = 0;

                foreach (GameObject p1 in roboparts)
                {
                    if (p.activeSelf)
                    {
                        nParts += 1;
                        centerX += p1.transform.position.x;
                        centerY += p1.transform.position.y;
                    }

                }

                centerX = centerX / nParts;
                centerY = centerY / nParts;

                foreach (GameObject p2 in roboparts)
                {
                    if (!p.Equals(p2) && p2.activeSelf)
                    {
                        float distance;


                        //Calculate Distance
                        distance = Mathf.Sqrt(Mathf.Pow(p2.transform.position.x - p.transform.position.x, 2) + Mathf.Pow(p2.transform.position.y - p.transform.position.y, 2));




                        //if distance is smallest AND connected part is closer to center than connecting part
                        if (distance < minDistance && Mathf.Sqrt(Mathf.Pow(p2.transform.position.x - centerX, 2) + Mathf.Pow(p2.transform.position.y - centerY, 2)) < Mathf.Sqrt(Mathf.Pow(p.transform.position.x - centerX, 2) + Mathf.Pow(p.transform.position.y - centerY, 2)))
                        {
                            minDistance = distance;
                            connectedPart = p2;
                        }

                    }
                }

                //If there are two, use the one that is closer to the center, if they are the same distance, use both
                //If its exists connect te two objects

                if (minDistance < float.MaxValue)
                {
 
                    GameObject newConnection;

                    if (!_3DConnections)
                    {
                        newConnection = GameObject.Instantiate(connection);
                    } else
                    {
                        newConnection = GameObject.Instantiate(connection3d);
                    }
                    newConnection.GetComponent<Connection>().SetupConnection(p.transform.position.x, p.transform.position.y, connectedPart.transform.position.x, connectedPart.transform.position.y);
                    newConnection.transform.position = new Vector3(newConnection.transform.position.x, newConnection.transform.position.y, p.transform.position.z);

                    if (newConnection.GetComponent<Connection>().length <= 1)
                    {
                        GameObject.Destroy(newConnection);
                    }
                    else
                    {

                        p.GetComponent<Robopart>().Connection = newConnection;
                    }

            }



            //Parent connection under Robot Prefab

            // Maybe: Store connection in LinkedList -> Use it later? (Break connections, Check which parts a Part is connected with)
        }
        }
    }

    public void ResetConnections()
    {
        DeleteAllConnections();
        if (rectangularConnections)
        {
            ConnectAllPartsRectangular();
        }
        else
        {
            ConnectAllParts();
        }
    }

    void DeleteAllConnections()
    {
        foreach (GameObject p in roboparts)
        {
            if (p.GetComponent<Robopart>().Connection != null)
            {
                GameObject.Destroy(p.GetComponent<Robopart>().Connection);
                p.GetComponent<Robopart>().Connection = null;
            }
            if (p.GetComponent<Robopart>().Connection2 != null)
            {
                GameObject.Destroy(p.GetComponent<Robopart>().Connection2);
                p.GetComponent<Robopart>().Connection2 = null;
            }
            if (p.GetComponent<Robopart>().ConnectionCube != null)
            {
                GameObject.Destroy(p.GetComponent<Robopart>().ConnectionCube);
                p.GetComponent<Robopart>().ConnectionCube = null;
            }
        }

    }


    //Translate Robot so lowest part is (nearly) touching the ground
    public void TranslateRobot()
    {
        //Debug.Log("WOOPWOOP");
        //Get Lowest Part
        float lowestPart = int.MaxValue;// int.MaxValue;

        foreach (GameObject p2 in roboparts)
        {
            if (p2.transform.position.y < lowestPart && p2.activeInHierarchy == true)
            {
                lowestPart = p2.transform.position.y;
            }
        }


        //Transform Robot to stand on ground
        //Debug.Log(lowestPart);

        transform.Translate(0, lowestPart * -1 + 0.5f, 0);
        //Optional Set height
    }



}

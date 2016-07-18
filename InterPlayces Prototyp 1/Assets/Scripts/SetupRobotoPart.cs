using UnityEngine;
using System.Collections;

public class SetupRobotoPart : MonoBehaviour {

    public GameObject robotoPart;
    public GameObject mrRoboto;


	// Use this for initialization
	void Start () {
        StartCoroutine(ImportRobotoPart());
	}

    public IEnumerator ImportRobotoPart()
    {
        yield return 1;

        GameObject newPart = null;

        switch (robotoPart.GetComponent<Robopart>().type)
        {

                
            case 0:
                
                newPart = InstantiatePart();
                
                break;
            case 1:
                newPart = InstantiatePart();
                newPart.transform.GetComponentInChildren<HingeJoint>().connectedBody = mrRoboto.GetComponent<Rigidbody>();
                newPart.transform.GetComponentInChildren<HingeJoint>().connectedAnchor = new Vector3(0, 0, 0);
                break;
            case 2:
                newPart = InstantiatePart();
               // newPart.transform.GetComponentInChildren<SpringJoint>().connectedBody = mrRoboto.GetComponent<Rigidbody>();
               // newPart.transform.GetComponentInChildren<SpringJoint>().connectedAnchor = new Vector3(0, 0, 0);
                break;
            case 3:
                newPart = InstantiatePart();
                newPart.GetComponentInChildren<PropellerPart>().ConnectHinges();
                break;
            case 4:
                newPart = InstantiatePart();
                newPart.GetComponentInChildren<JetPart>().ResetJet();
                break;

        }

        //return newPart;
    }


    GameObject InstantiatePart()
    {

    
        GameObject newPart = GameObject.Instantiate(robotoPart);
        newPart.transform.SetParent(this.transform);
        newPart.transform.localPosition = new Vector3(0, 0, 0);
        newPart.transform.localRotation = Quaternion.identity;
        //Debug.Log(Parts.Instance.ToString());
        Parts.Instance.AddRobopart(newPart);
       // Debug.Log("TESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTESTTESTTESTTTEST");
        


        //newPart.transform.position = new Vector3(x, y, 0);
        //newPart.transform.Rotate(Vector3.forward * rotation * -1);

        //TODO: Add Tangibles 

        return newPart;
    }

    //IEnumerator AddPart()
    //{

    //}

    // Update is called once per frame
    void Update () {
	
	}


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

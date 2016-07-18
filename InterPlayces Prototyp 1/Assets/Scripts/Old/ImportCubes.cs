using UnityEngine;
using System.Collections;

public class ImportCubes : MonoBehaviour {

    private static ImportCubes instance;
    public static ImportCubes Instance
    {
        get { return instance; }
    }

    public GameObject partType0; //Base
    public GameObject partType1; //Hinge
    public GameObject partType2; //Spring
    public GameObject mrRoboto;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	}


 


    public GameObject ImportCube (float x, float y, float rotation, int type)
    {


        //TODO: IF there already is a part on that position, dont import (not probable, as everything is represented by physical objects)

        GameObject newPart = null;
        
        switch (type)
        {
            case 0:

                
                newPart = InstantiatePart(x,y,rotation, partType0);
                break;
            case 1:
                newPart = InstantiatePart(x, y, rotation, partType1);
                newPart.transform.GetComponentInChildren<HingeJoint>().connectedBody = mrRoboto.GetComponent<Rigidbody>();
                newPart.transform.GetComponentInChildren<HingeJoint>().connectedAnchor = new Vector3 (0,0,0);
                break;
            case 2:
                newPart = InstantiatePart(x, y, rotation, partType2);
                newPart.transform.GetComponentInChildren<SpringJoint>().connectedBody = mrRoboto.GetComponent<Rigidbody>();
                newPart.transform.GetComponentInChildren<SpringJoint>().connectedAnchor = new Vector3(0, 0, 0);
                break;

        }

        return newPart;
    }

    GameObject InstantiatePart(float x, float y, float rotation, GameObject part)
    {
        GameObject newPart = GameObject.Instantiate(part);
        
        Parts.Instance.AddRobopart(newPart);
        newPart.transform.SetParent(mrRoboto.transform);


        newPart.transform.position = new Vector3(x, y, 0);
        newPart.transform.Rotate(Vector3.forward * rotation * -1);

        //TODO: Add Tangibles 

        return newPart;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class TangibleCube {

}

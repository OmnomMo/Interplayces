using UnityEngine;
using System.Collections;

public class PropellerPart : MonoBehaviour {

    public bool isTurning;
    public GameObject fans;

    public float force;
    

	// Use this for initialization
	void Start () {
        isTurning = false;
	}
	
	// Update is called once per frame
	void Update () {
	

        if (isTurning)
        {

            Parts.Instance.GetComponent<Rigidbody>().AddRelativeForce(0, force * Time.deltaTime, 0);
            fans.transform.Rotate(new Vector3(0, 1, 0), 720 * Time.deltaTime);
            
        } else
        {
           // Parts.Instance.GetComponent<ConstantForce>().force = new Vector3(0, 0, 0);
        }
       

	}

    public void ConnectHinges()
    {
        //transform.GetComponentInChildren<HingeJoint>().transform.localPosition = Hingeposition;
        // transform.GetComponentInChildren<HingeJoint>().transform.rotation = Quaternion.identity;

        //transform.GetComponentInChildren<HingeJoint>().connectedBody = Parts.Instance.transform.GetComponent<Rigidbody>();
        //transform.GetComponentInChildren<HingeJoint>().connectedAnchor = new Vector3(0, 0, 0);
    }
}

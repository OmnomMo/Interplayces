using UnityEngine;
using System.Collections;

public class HingePart : MonoBehaviour {

    public Vector3 Hingeposition;

    public GameObject hinge;
	// Use this for initialization
	void Start () {
	
	}

    public void UnHinge()
    {
        hinge.GetComponent<HingeJoint>().useLimits = false;
        StartCoroutine(StopMotor());
    }

    IEnumerator StopMotor()
    {
        
        yield return new WaitForSeconds(1f);
        hinge.GetComponent<HingeJoint>().useMotor = false;
    }

    public void ReHinge()
    {
        hinge.GetComponent<HingeJoint>().useLimits = true;
        hinge.GetComponent<HingeJoint>().useMotor = true;
    }

    public void ConnectHinges()
    {
        transform.GetComponentInChildren<HingeJoint>().transform.localPosition = Hingeposition;
       // transform.GetComponentInChildren<HingeJoint>().transform.rotation = Quaternion.identity;

        transform.GetComponentInChildren<HingeJoint>().connectedBody = Parts.Instance.transform.GetComponent<Rigidbody>();
        transform.GetComponentInChildren<HingeJoint>().connectedAnchor = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
	}
}

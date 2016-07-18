using UnityEngine;
using System.Collections;

public class SpringPart : MonoBehaviour
{

    public Vector3 Springposition;
    public float force;

    public GameObject hinge;
    // Use this for initialization
    void Start()
    {

    }

    public void UnSpring()
    {
        //SpringJoint spring = GetComponentInChildren<SpringJoint>();
        //spring.minDistance = 1;
        //spring.maxDistance = 1;
        //spring.spring = 10000;
        //spring.GetComponent<Rigidbody>().isKinematic = false;
        //Debug.Log("Sptinggg!");
        Parts.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0));
        transform.FindChild("SPring").transform.localPosition = new Vector3(0, -0.84f, 0);
    }

    public void ReSpring()
    {
        //SpringJoint spring = GetComponentInChildren<SpringJoint>();
        //spring.minDistance = 0;
        //spring.maxDistance = 0.001f;
        //spring.spring = 1000;
        //spring.GetComponent<Rigidbody>().isKinematic = true;
        transform.FindChild("SPring").transform.localPosition = new Vector3(0, -0.48f, 0);
    }


    public void ConnectHinges()
    {

        //SpringJoint spring = transform.FindChild("Base").GetComponent<SpringJoint>();

        //spring.transform.localPosition = Springposition;
        //spring.transform.rotation = Quaternion.identity;
        //spring.connectedBody = Parts.Instance.transform.GetComponent<Rigidbody>();
        //spring.connectedAnchor = new Vector3(0, 0, 0);
    
    }

    // Update is called once per frame
    void Update()
    {
    }
}
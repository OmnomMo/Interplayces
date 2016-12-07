using UnityEngine;
using System.Collections;

public class ScpaceshipMovement : MonoBehaviour {


    public float thrust;
    bool isAccelerating;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //isAccelerating = Input.GetAxis("Vertical") > 0;

        transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAccelerating = true;
        } else
        {
            isAccelerating = false;
        }
    }


    void FixedUpdate()
    {
        if (isAccelerating)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
        }
    }
}

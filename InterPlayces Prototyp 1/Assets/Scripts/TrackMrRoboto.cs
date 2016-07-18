using UnityEngine;
using System.Collections;

public class TrackMrRoboto : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetposition = new Vector3((Parts.Instance.transform.position.x + Parts.Instance.GetComponent<Rigidbody>().centerOfMass.x) * 0.9f, (Parts.Instance.transform.position.y + Parts.Instance.GetComponent<Rigidbody>().centerOfMass.y)*0.975f + 2, -10 - (Parts.Instance.transform.position.y + Parts.Instance.GetComponent<Rigidbody>().centerOfMass.y) * 0.0f);
        float t = Time.deltaTime;
        float step = speed * t;

       // GetComponent<Camera>().orthographicSize = Mathf.Sqrt(Mathf.Sqrt(Mathf.Pow(Parts.Instance.transform.position.x + Parts.Instance.GetComponent<Rigidbody>().centerOfMass.x, 2)
       //     + Mathf.Pow((Parts.Instance.transform.position.y + Parts.Instance.GetComponent<Rigidbody>().centerOfMass.y), 2)) * 4);  

        transform.position = Vector3.MoveTowards(transform.position, targetposition, step);

	}
}

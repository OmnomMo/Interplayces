using UnityEngine;
using System.Collections;

public class Robopart : MonoBehaviour {

    private TangibleCube tangible {
        get; set;
    }

    public GameObject Connection;
    public GameObject Connection2;
    public GameObject ConnectionCube;

    public GameObject correspondingPart;

    public int type;
    

	// Use this for initialization
	void Start () {
	
	}

    public void UpdatePosition(float x, float y, float rotation)
    {
        Debug.Log("Update Position of " + gameObject.ToString() + " to " + rotation);
        transform.position = new Vector3(x, y, 0);
        transform.rotation = Quaternion.identity;
        transform.Rotate(   Vector3.forward * rotation * -1);

        Parts.Instance.ResetConnections();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

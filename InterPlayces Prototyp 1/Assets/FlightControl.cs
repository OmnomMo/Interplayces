using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlightControl : MonoBehaviour {

    public float rotationspeed;

    public int nBoosts;
    int remainingBoosts;

    public GameObject boostButton;


    private static FlightControl instance;
    public static FlightControl Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
       
    }


    public void Boost()
    {
        if (gameObject.GetComponent<PlayStateManager>().jumping)
        {
            if (Parts.Instance.transform.GetComponentInChildren<JetPart>() != null)
            {
                if (Parts.Instance.transform.GetComponentInChildren<JetPart>().isFiring == false)
                {
                    if (--remainingBoosts >= 0)
                    {
                        Parts.Instance.ActivateBoost();
                        UpdateBoostButton(remainingBoosts);
                    }

                    if (remainingBoosts == 0)
                    {
                        boostButton.GetComponent<Button>().enabled = false;
                        boostButton.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
                    }
                }
            }
        }

   
    }


    public void UpdateBoostButton (int n)
    {
        boostButton.transform.GetComponentInChildren<Text>().text = "x" + remainingBoosts ;
    }

    public void SteerLeft() {
        if (UpdateInterface.Instance.height > 5)
        {
            Parts.Instance.GetComponent<Rigidbody>().AddTorque(Vector3.forward * rotationspeed);
            //Parts.Instance.transform.Rotate(rotationspeed * Vector3.forward * Time.deltaTime);
        }
    }

    public void SteerRight()
    {
        if (UpdateInterface.Instance.height > 5)
        {
            Parts.Instance.GetComponent<Rigidbody>().AddTorque(Vector3.forward * rotationspeed * -1);
            //Parts.Instance.transform.Rotate(-rotationspeed * Vector3.forward * Time.deltaTime);
        }
    }

    // Use this for initialization
    void Start () {
        ResetBoosts();

   

    }

    public void ResetBoosts()
    {
        remainingBoosts = nBoosts;

        
        UpdateBoostButton(remainingBoosts);

        if (Parts.Instance.transform.GetComponentInChildren<JetPart>() == null)
        {
            boostButton.GetComponent<Button>().enabled = false;
            boostButton.GetComponentInChildren<Text>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            boostButton.GetComponentInChildren<Text>().color = new Color(1f, 1f, 1f);
            boostButton.GetComponent<Button>().enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

   
	}
}

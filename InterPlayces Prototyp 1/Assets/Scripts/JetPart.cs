using UnityEngine;
using System.Collections;

public class JetPart : MonoBehaviour {


    public GameObject particles;
    public bool isFiring;
    public float force;

    public float boostForce;

     float currentForce;

	// Use this for initialization
	void Start () {
	
	}

    public void StartEngines()
    {

        //Debug.Log("FIRESEENGINGES!");

        isFiring = true;
        particles.SetActive(true);

        currentForce = force;

        StartCoroutine(FireEngines(2f));
    }

    public void StartBoost()
    {

        //Debug.Log("FIRESEENGINGES!");

        currentForce = boostForce;

        isFiring = true;
        particles.SetActive(true);

        StartCoroutine(FireEngines(1f));
    }

    public IEnumerator FireEngines(float t)
    {

        

        yield return new WaitForSeconds(t);

        //Debug.Log("STOPSEENGINES");
        particles.SetActive(false);

        isFiring = false;

    }

    public void ResetJet()
    {
        particles.SetActive(false);

        isFiring = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (isFiring)
        {
            Parts.Instance.GetComponent<Rigidbody>().AddRelativeForce(0, currentForce * Time.deltaTime, 0);
        }
	
	}
}

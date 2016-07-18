using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown("space")) {
            //Unhinge

            if (GameState.Instance.IsInPlay())
            {
                if (!GetComponent<PlayStateManager>().jumping)
                {
                    Jump();
                } else
                {
           
                    FlightControl.Instance.Boost();
                
                }
                
            }

           
        }

        if (Input.GetKeyDown("return"))
        {
            //Unhinge
            if (GameState.Instance.IsInSetup())
            {
                GameState.Instance.SwitchToPlay();

               
            }


        }

         if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {

            Debug.Log("RightRightRight!!");
            FlightControl.Instance.SteerRight();
        }


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            FlightControl.Instance.SteerLeft();
        }
    }

    public void Jump()
    {
        Parts.Instance.ActivateAllParts();
        UpdateInterface.Instance.EnableReset();
        GetComponent<PlayStateManager>().jumping = true;
    }


}

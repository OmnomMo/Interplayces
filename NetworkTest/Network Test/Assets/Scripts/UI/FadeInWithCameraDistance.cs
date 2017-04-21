using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object fades in or out depending on camera distance


public class FadeInWithCameraDistance : MonoBehaviour {


    public float invisibleDistance, visibleDistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (visibleDistance > invisibleDistance)
        {
            if (Camera.main.transform.position.y >  visibleDistance)
            {
                GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            } else
            {
                if (Camera.main.transform.position.y < invisibleDistance)
                {
                    GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
                } else
                {

                    //if camera distance is between visible and invisible poits, change opacity depending on distance
                    GetComponent<Renderer>().material.color = new Color(1, 1, 1, (Camera.main.transform.position.y - invisibleDistance) / (float)( visibleDistance - invisibleDistance )  );
                }
            }


        } else //Íf visible distance is lower than invisible distance -> fade out when zoomed out
        {
            if (Camera.main.transform.position.y < visibleDistance)
            {
                GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            }
            else
            {
                if (Camera.main.transform.position.y > invisibleDistance)
                {
                    GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
                }
                else
                {

                    //if camera distance is between visible and invisible poits, change opacity depending on distance
                    GetComponent<Renderer>().material.color = new Color(1, 1, 1, (invisibleDistance - Camera.main.transform.position.y) / (float)(invisibleDistance - visibleDistance));
                }
            }
        }


	}
}

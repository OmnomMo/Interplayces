using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjective : Objective {

    
    public float timeToCompletion;

	// Use this for initialization
	

	
	// Update is called once per frame
	void Update () {

        if (!completed && parentObjective == null)
        {
            ShowStartTooltip();


            ShowHelpTooltip();
        }

        if (started && Time.time - timeToCompletion >= startingTime)
        {
            Complete();
        }
	}
}

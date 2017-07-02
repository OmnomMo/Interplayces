using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAreaObjective : Objective {


    public ClearArea areaToClear;


	
	// Update is called once per frame
	void Update () {
		
        if (areaToClear.clear)
        {
            base.Complete();
        }

	}
}

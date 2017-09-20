using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAreaObjective : Objective {


    public ClearArea areaToClear;


    new public int GetPriority()
    {
        return (int)Vector3.Distance(areaToClear.transform.position, SpaceshipGameplay.Instance.transform.position);
    }

    void Start()
    {
        toolTipTarget = areaToClear.transform;
        base.Start();
    }

	// Update is called once per frame
	void Update () {

        if (!completed && parentObjective == null)
        {
            ShowStartTooltip();


            ShowHelpTooltip();
        }

        if (areaToClear.clear && !completed)
        {
            Debug.Log("Complete ClearAreaObjective");
           Complete();
        }

	}
}

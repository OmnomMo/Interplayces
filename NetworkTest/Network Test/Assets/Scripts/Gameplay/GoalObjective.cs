using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObjective : Objective {

    public Collider goal;


    new public int GetPriority()
    {
        return (int) Vector3.Distance(goal.transform.position, SpaceshipGameplay.Instance.transform.position);
    }

	// Use this for initialization
	new void Start () {
        toolTipTarget = goal.transform;
        base.Start();
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}

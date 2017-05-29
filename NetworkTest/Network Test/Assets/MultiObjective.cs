using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObjective : Objective {


    public Objective[] subObjectives;

	// Use this for initialization
	void Start () {
		
	}

    public void Complete()
    {
        bool allCompleted = true;

        foreach (Objective o in subObjectives)
        {
            if (!o.IsCompleted())
            {
                allCompleted = false;
            }
        }

        if (allCompleted) { completed = true; }
    }

	// Update is called once per frame
	void Update () {
		
	}
}

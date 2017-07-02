using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObjective : Objective {


    public Objective[] subObjectives;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    new public bool Complete()
    {
        Debug.Log("Try to complete MultiObjective: " + description);

        bool allCompleted = true;

        foreach (Objective o in subObjectives)
        {
            if (!o.IsCompleted())
            {
                Debug.Log("Not all objectives are completed!");
                allCompleted = false;
            }
        }

        if (allCompleted) { completed = true;
            OnCompletion();
        }

        

        return allCompleted;
    }

    new public void OnCompletion()
    {
        base.OnCompletion();
    }

	// Update is called once per frame
	void Update () {
		
	}
}

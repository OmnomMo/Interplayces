using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour {


    private static LevelProgress instance;
    public static LevelProgress Instance
    {
        get { return instance; }
    }

    public Objective[] objectives;

	// Use this for initialization
	void Start () {
		if (LevelProgress.Instance == null)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class Objective : MonoBehaviour
{

    public bool finalObjective;

    public Objective prerequisite;
    public Objective parentObjective;
    public bool completed;

    public void SetPrerequisite(Objective prerequisite_)
    {
        prerequisite = prerequisite_;
    }
    public Objective GetPrerequisite()
    {
        return prerequisite;
    }
    public bool HasPrerequisite()
    {
        return prerequisite != null;
    }

    public bool Complete()
    {
        if (!HasPrerequisite() || (HasPrerequisite() && prerequisite.IsCompleted())) {
            completed = true;
            OnCompletion();
            return true;
        }

        return false;
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public void OnCompletion()
    {
        if (parentObjective != null)
        {
            parentObjective.Complete();
        }

        if (finalObjective)
        {
            ToEndScreen.Instance.EndWin();
        }
    }
}



﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        HideObjectivesForNavigator();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideObjectivesForNavigator()
    {
        //if (GameState.Instance.isPlayerNavigator())
        //{
        //    Transform[] ObjectiveInterfaces = transform.GetComponentsInChildren<Transform>();

        //    foreach (Transform t in ObjectiveInterfaces)
        //    {
        //        t.gameObject.SetActive(false);
        //    }
        //}
    }


}


public class Objective : MonoBehaviour
{

    public bool finalObjective;

    public Objective prerequisite;
    public List<Objective> followingObjectives;

    public bool prerequisiteDone;



    public Objective parentObjective;

    public GameObject backgroundPanel;
    public GameObject descriptionObject;

    public string description;


    public bool completed;


    protected void Start()
    {
        if (HasPrerequisite() || ((parentObjective != null) && (parentObjective.HasPrerequisite())) )
        {
            prerequisiteDone = false;
            descriptionObject.GetComponent<Text>().color = new Color(0.5f, 0.5f, 0.5f);

            
        }

        if (descriptionObject != null)
        {
           // Debug.Log("Set description Text to: " + description);
            descriptionObject.GetComponent<Text>().text = description;
        }

        if (prerequisite != null)
        {
            prerequisite.followingObjectives.Add(this);
        }
    }

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

        //Debug.Log("Try to complete: " + description);
        if (!HasPrerequisite() || (HasPrerequisite() && prerequisiteDone)) {


            if (parentObjective == null || (!parentObjective.HasPrerequisite() || parentObjective.prerequisiteDone))
            {
                completed = true;
                OnCompletion();
                return true;
            }
        }

        

        return false;
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public void OnCompletion()
    {


        if (backgroundPanel != null)
        {
            backgroundPanel.GetComponent<Image>().color = new Color(0.2f, 1f, 0.2f);
        }

        if (descriptionObject != null)
        {
            descriptionObject.GetComponent<Text>().color = new Color(0.2f, 1f, 0.2f);
        }

        if (followingObjectives.Count > 0)
        {
            foreach (Objective o in followingObjectives)
            {
                o.prerequisiteDone = true;
                o.descriptionObject.GetComponent<Text>().color = new Color(1f, 1f, 1f);

                if (o is MultiObjective)
                {

                    MultiObjective mO = (MultiObjective)o;

                    foreach (Objective sO in mO.subObjectives)
                    {
                        sO.descriptionObject.GetComponent<Text>().color = new Color(1f, 1f, 1f);
                    }
                }
            }
        }


        if (parentObjective != null)
        {
            MultiObjective parent = (MultiObjective)parentObjective;

            parent.Complete();
        }

        if (finalObjective)
        {
            ToEndScreen.Instance.EndWin();
        }
    }
}



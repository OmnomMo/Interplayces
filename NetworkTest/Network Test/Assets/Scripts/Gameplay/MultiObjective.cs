using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObjective : Objective {


    public Objective[] subObjectives;

    public float timeCompleteLastSubObj;
    public float Timetime;


    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    new public bool Complete()
    {
        Debug.Log("Try to complete MultiObjective: " + description);

        timeCompleteLastSubObj = Time.time;
        helpTooltip.Hide();

        bool allCompleted = true;

        foreach (Objective o in subObjectives)
        {
            if (!o.IsCompleted())
            {
                Debug.Log("Not all objectives are completed!");
                allCompleted = false;
                helpShown = false;
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
    void Update()
    {
        Timetime = Time.time;

        if (!completed && parentObjective == null)
        {
            ShowStartTooltip();


            ShowHelpTooltip();
        }

    }

    new public void ShowStartTooltip()
    {
        if (active && !started)
        {

          
            timeCompleteLastSubObj = Time.time;
        }

        base.ShowStartTooltip();

    }

    new public int GetPriority()
    {
        return GetPriorityObjective().GetPriority();
    }

    new public void ShowHelpTooltip()
    {
      
        if (Time.time - timeCompleteLastSubObj > timeToHelpTooltip && !helpShown) //(helpTooltip == null || !helpTooltip.gameObject.activeInHierarchy))
        {
            helpShown = true;
            Objective priorityObjective = GetPriorityObjective();

           helpTooltip = TooltipManager.Instance.NewTooltip(priorityObjective.helpTtText, priorityObjective.helpTtSprite, 0, priorityObjective.toolTipTarget);

        }
        
    }

    public Objective GetPriorityObjective()
    {
        int lowestPriority = int.MaxValue;
        Objective priorityObjective = null;
        foreach (Objective obj in subObjectives)
        {
           // Debug.Log("Priority of Objective " + obj.ToString() + ": " + obj.GetPriority());
            if (obj.GetPriority() < lowestPriority & !obj.completed)
            {
                priorityObjective = obj;
                lowestPriority = obj.GetPriority();
            }
        }

        return priorityObjective;
    }
}

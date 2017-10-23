using System.Collections;
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


    public Objective GetCurrentObjective ()
    {
        foreach (Objective obj in objectives)
        {
            if (obj.active)
            {
                return obj;
            }

            
        }

        return null;
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



    public MultiObjective parentObjective;

    public GameObject backgroundPanel;
    public GameObject descriptionObject;

    public string description;

    public bool active;
    public bool started = false;
    public float startingTime;

    public bool completed;


    protected Tooltip startTooltip;
    public string startTtText;
    public Sprite startTtSprite;


    protected Tooltip helpTooltip;
    public bool helpShown;
    public string helpTtText;
    public Sprite helpTtSprite;


    public int startTooltipTime;
    public int timeToHelpTooltip;
    public Transform toolTipTarget;
    
    void Update()
    {
        if (!completed && parentObjective == null)
        {
            ShowStartTooltip();


            ShowHelpTooltip();
        }

    }

    public Transform GetTarget()
    {
        return toolTipTarget;
    }

    //returns priority of Ojective. The lower the more important
    public virtual int GetPriority()
    {
        return 100;
    }

    public virtual void ShowHelpTooltip()
    {
        if (started && timeToHelpTooltip != 0)
        {
            if (Time.time - startingTime > timeToHelpTooltip && !helpShown) // (helpTooltip == null || !helpTooltip.gameObject.activeInHierarchy))
            {
                helpShown = true;
                helpTooltip = TooltipManager.Instance.NewTooltip(helpTtText, helpTtSprite, 0, toolTipTarget);

            }
        }
        
    }
    

    public virtual void ShowStartTooltip()
    {
        if (active && !started)
        {

            //Debug.Log("Show start Tooltip for " + gameObject.ToString());
            TooltipManager.Instance.infoSource.Play();

            //startTooltip = TooltipManager.Instance.NewConfirmTooltip(this, startTtText);
            startTooltip = TooltipManager.Instance.NewTooltip(startTtText, startTtSprite, startTooltipTime, toolTipTarget);

                
            

            startingTime = Time.time;
            started = true;
        }

        
    }

    

    protected void Start()
    {
        if (HasPrerequisite() || ((parentObjective != null) && (parentObjective.HasPrerequisite())) )
        {
            prerequisiteDone = false;

            if (descriptionObject != null)
            {
                descriptionObject.GetComponent<Text>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            active = false;

            
        } else
        {
            active = true;
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
                //Debug.Log("Call OnCompletion");
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

    public void DeactivateTooltips()
    {

       // Debug.Log("Deactivate Tooltips of Quest");

        if (startTooltip != null)
        {
            startTooltip.Hide();
        }

        if (helpTooltip != null)
        {
            helpTooltip.Hide();
        }
    }

    public void OnCompletion()
    {
        active = false;

        DeactivateTooltips();
        

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
                if (descriptionObject != null)
                {
                    o.descriptionObject.GetComponent<Text>().color = new Color(1f, 1f, 1f);
                }
                o.active = true;

                if (o is MultiObjective)
                {

                    MultiObjective mO = (MultiObjective)o;

                    foreach (Objective sO in mO.subObjectives)
                    {
                        if (descriptionObject != null)
                        {
                            sO.descriptionObject.GetComponent<Text>().color = new Color(1f, 1f, 1f);
                        }
                        sO.active = true;
                    }
                }
            }
        }


        if (parentObjective != null)
        {
            //MultiObjective parent = (MultiObjective)parentObjective;

            //parent.Complete();
            Debug.Log("Try to complete parent Objective");
            parentObjective.Complete();
        }

        if (finalObjective)
        {
            ToEndScreen.Instance.EndWin();
        }
    }
}



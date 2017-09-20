using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObjective : Objective {

    public int nRequired;

    public int nCurrent;

    public float timeLastPickup;

    //public ResourcePickup[] pickups;


	// Use this for initialization
	new void Start () {
        nCurrent = 0;
        base.Start();
        descriptionObject.GetComponent<Text>().text = description + " (" + nCurrent + "/" + nRequired + ")";
    }

    new public bool Complete()
    {

        Debug.Log("Trying to complete pickup objective");

        descriptionObject.GetComponent<Text>().text = description + " (" + nCurrent + "/" + nRequired + ")";

        if (nCurrent >= nRequired)
        {
            base.Complete();
            //OnCompletion();
            return true;
        }
        else
        {
            timeLastPickup = Time.time;

            if (helpTooltip != null)
            {
                helpTooltip.Hide();
            }
            return false;
        }
    }


    new public void OnCompletion()
    {
        base.OnCompletion();
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed && parentObjective == null)
        {
            ShowStartTooltip();
            ShowPickupHelpTooltip();
        }
    }

    new public void ShowStartTooltip()
    {
        if (active && !started)
        {
            timeLastPickup = Time.time;
        }

        base.ShowStartTooltip();
       
    }

    public void ShowPickupHelpTooltip()
    {
        if (helpTooltip != null)
        {
            if (Time.time - timeLastPickup > timeToHelpTooltip && !helpTooltip.gameObject.activeInHierarchy)
            {
                helpTooltip.gameObject.SetActive(true);
                helpTooltip.SetTTVisibility(true, true);
                helpTooltip.SetTTArrowTarget(GetNearestPickup());
                helpTooltip.Show(helpTtText, helpTtSprite);
            }

        }
    }

    public Transform GetNearestPickup()
    {
        float d = float.MaxValue;
        Transform closestPickup = null;


        foreach (ResourcePickup p in PickupManager.Instance.resourcePickups)
        {
            float dNew = Vector3.Distance(p.gameObject.transform.position, SpaceshipGameplay.Instance.transform.position);

            if (dNew < d && !p.pickedUp)
            {
                d = dNew;
                closestPickup = p.transform;
            }
        }

        return closestPickup;
    }

    public void HideHelpPickup()
    {
        if (helpTooltip != null)
        {
            helpTooltip.gameObject.SetActive(false);
        }
    }
}

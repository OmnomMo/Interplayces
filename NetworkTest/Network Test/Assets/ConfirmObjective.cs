using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmObjective : Objective {

    

    public override void ShowStartTooltip()
    {
        if (active && !started)
        {


            TooltipManager.Instance.infoSource.Play();

           // startTooltip = TooltipManager.Instance.NewTooltip(startTtText, startTtSprite, startTooltipTime, toolTipTarget);
           startTooltip = TooltipManager.Instance.NewConfirmTooltip(this, startTtText);

           // Debug.Log("Show Tooltip: " + startTooltip.ttText);


            startingTime = Time.time;
            started = true;
        }
    }


}

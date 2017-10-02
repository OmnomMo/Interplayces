using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmObjective : Objective {



    public override void ShowStartTooltip()
    {
        if (active && !started)
        {





            startTooltip = TooltipManager.Instance.NewConfirmTooltip(this, startTtText);




            startingTime = Time.time;
            started = true;
        }
    }


}

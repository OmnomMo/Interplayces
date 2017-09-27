using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InterPlayces
{

    public class Debug : MonoBehaviour
    {

        int nTestTooltip;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("u"))
            {
                TooltipManager.Instance.NewTooltip("Testtooltip: " + nTestTooltip, null, 10);
                nTestTooltip++;
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sessionmanagement : MonoBehaviour {

    

    public bool waitForInput;

    public float timeToRestart;
    public float timeToTooltip;
    public float timeLastInput;
    public System.DateTime dateTimeLastInput;
    public Tooltip warningTooltip;

    private static Sessionmanagement instance;

    public static Sessionmanagement Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}

    public void StartSession()
    {

        DataCollection.Instance.StartSession();

        if (!GameState.Instance.skipBuildingPhase)
        {
            MultiplayerSetup.Instance.ServerChangeScene("02_SpaceShipEditor_Tracking");
        }
        else
        {
            MultiplayerSetup.Instance.ServerChangeScene("03_Level_Select");
        }
    }

    public void EndSession(System.DateTime endTime)
    {
        waitForInput = true;

        DataCollection.Instance.EndSession(endTime);


        MultiplayerSetup.Instance.ServerChangeScene("01b_LandingScreen");


    }
	
    public void GetNewInput()
    {
        if (waitForInput)
        {
            waitForInput = false;
            StartSession();
        }

        if (TooltipManager.Instance != null && warningTooltip != null)
        {
            TooltipManager.Instance.RemoveToolTipFromQueue(warningTooltip);
            warningTooltip = null;
        }

        timeLastInput = Time.time;
        dateTimeLastInput = System.DateTime.Now;




    }

	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            GetNewInput();
        }

        if (Time.time - timeLastInput >= timeToTooltip)
        {
            if (!waitForInput)
            {
                if (TooltipManager.Instance != null && warningTooltip == null) {
                    int timeLeft = (int)timeToRestart - (int)timeToTooltip;
                    warningTooltip = TooltipManager.Instance.NewTooltip("Achtung! Das Spiel wird in " + timeLeft + " Sekunden neu gestartet. Wenn ihr weiterspielen möchtet drückt bitte irgendeinen Knopf!", null, timeLeft);
                    TooltipManager.Instance.warningSource.Play();
                        }
            }
        }


        if (Time.time - timeLastInput >= timeToRestart)
        {
            if (!waitForInput)
            {
                EndSession(dateTimeLastInput);
                waitForInput = true;
            }
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTooltipManager : MonoBehaviour {
    
     Tooltip lowHealthTooltip;
    public Sprite lowHealthTtSprite;
    bool lowHealthInfoGiven;
    public Tooltip lowEnergyTooltip;
    public bool showLowEnergyTooltip;
    public Sprite lowEnergyTtSprite;

     Tooltip highscoreTooltip;
    public Sprite highscoreTtSprite;
    bool highscoreInfoGiven;

    public Sprite congratulationsSprite;

    public int levelMaxScore;

	// Use this for initialization
	void Start () {

        levelMaxScore = 100;
        StartCoroutine(GetScore());


    }

    public IEnumerator GetScore()
    {
        yield return null;
        int testScore = GameObject.FindGameObjectsWithTag("Planet").Length;
        if (testScore != 0)
        {
            levelMaxScore = testScore;
        } else
        {
            StartCoroutine(GetScore());
        }
        Debug.Log("Total Possible score: " + levelMaxScore);
    }


    public void ShowLowEnergyTooltip()
    {
        if (lowEnergyTooltip == null) // || !lowEnergyTooltip.isActiveAndEnabled)
        {
            showLowEnergyTooltip = true;
            lowEnergyTooltip = TooltipManager.Instance.NewTooltip("lowEnergyTooltipText", lowEnergyTtSprite);
        } 
    }

     public void ShowLowHealthTooltip()
    {
        if (lowHealthTooltip == null || !lowHealthTooltip.isActiveAndEnabled)
        {

            lowHealthTooltip = TooltipManager.Instance.NewTooltip("lowHealthTooltipText", lowHealthTtSprite, 30);
            
        }
    }

    public void ShowLevelHighScoreTooltip()
    {
        if (highscoreTooltip == null || !highscoreTooltip.isActiveAndEnabled)
        {


            highscoreTooltip = TooltipManager.Instance.NewTooltip("allPlanetsFountTooltipText", null, 30);

        }
    }



    // Update is called once per frame
    void Update () {


		if (SpaceshipGameplay.Instance.energy / (float) SpaceshipGameplay.Instance.energyCapacity < 0.2f)
        {
            // Debug.Log("Show Energy Tooltip");
            if (!showLowEnergyTooltip)
            {
                ShowLowEnergyTooltip();
            }
        } else
        {
            if (showLowEnergyTooltip && lowEnergyTooltip != null)
            {
                lowEnergyTooltip.Hide();
                lowEnergyTooltip = null;
                showLowEnergyTooltip = false;
            }
        }

        if (Time.timeSinceLevelLoad > 20 && !lowHealthInfoGiven && SpaceshipGameplay.Instance.hitPoints / (float)SpaceshipGameplay.Instance.maxHitpoints < 0.2f)
        {
           // Debug.Log("Show Health Tooltip");
            ShowLowHealthTooltip();
            lowHealthInfoGiven = true;
        }



        if (Time.timeSinceLevelLoad > 5 && !highscoreInfoGiven && Score.Instance.currentScore >= levelMaxScore)
        {

            highscoreInfoGiven = true;
            ShowLevelHighScoreTooltip();
        }
    }
}

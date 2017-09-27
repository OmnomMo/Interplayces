using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTooltipManager : MonoBehaviour {
    
     Tooltip lowHealthTooltip;
    public Sprite lowHealthTtSprite;
    bool lowHealthInfoGiven;
     Tooltip lowEnergyTooltip;
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
        levelMaxScore = GameObject.FindGameObjectsWithTag("Planet").Length;
    }


    public void ShowLowEnergyTooltip()
    {
        if (lowEnergyTooltip == null || !lowEnergyTooltip.isActiveAndEnabled)
        {

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
            Debug.Log("Show Energy Tooltip");
            ShowLowEnergyTooltip();
        } else
        {
            if (lowEnergyTooltip != null)
            {
                lowEnergyTooltip.Hide();
            }
        }

        if (Time.time - Time.timeSinceLevelLoad > 1 && !lowHealthInfoGiven && SpaceshipGameplay.Instance.hitPoints / (float)SpaceshipGameplay.Instance.maxHitpoints < 0.2f)
        {
            Debug.Log("Show Health Tooltip");
            ShowLowHealthTooltip();
            lowHealthInfoGiven = true;
        }



        if (!highscoreInfoGiven && Score.Instance.currentScore >= levelMaxScore)
        {

            highscoreInfoGiven = true;
            ShowLevelHighScoreTooltip();
        }
    }
}

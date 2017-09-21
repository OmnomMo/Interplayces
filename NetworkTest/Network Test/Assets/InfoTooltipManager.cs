using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTooltipManager : MonoBehaviour {
    
    public Tooltip lowHealthTooltip;
    public Sprite lowHealthTtSprite;
    public Tooltip lowEnergyTooltip;
    public Sprite lowEnergyTtSprite;

    public Tooltip highscoreTooltip;
    public Sprite highscoreTtSprite;

    public Sprite congratulationsSprite;
    public string congratulationsString;

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
        if (lowEnergyTooltip != null && !lowEnergyTooltip.isActiveAndEnabled)
        {


            lowEnergyTooltip.gameObject.SetActive(true);
            lowEnergyTooltip.SetTTVisibility(true, true);
            lowEnergyTooltip.SetTTArrowTarget(null);
            lowEnergyTooltip.Show("Achtung, eure Energie ist niedrig! Sammelt sobald wie möglich einen Energiebehälter auf!", lowEnergyTtSprite);
        } 
    }

     public void ShowLowHealthTooltip()
    {
        if (lowHealthTooltip != null && !lowHealthTooltip.isActiveAndEnabled)
        {

            
                lowHealthTooltip.gameObject.SetActive(true);
                lowHealthTooltip.SetTTVisibility(true, true);
                lowHealthTooltip.SetTTArrowTarget(null);
                lowHealthTooltip.Show("Achtung, euer Raumschff hält nicht mehr viel aus! Vermeidet Zusammenstöße mit Asteroiden oder aktiviert eure Schilde!", lowHealthTtSprite);
            
        }
    }

    public void ShowLevelHighScoreTooltip()
    {
        if (highscoreTooltip != null && !highscoreTooltip.isActiveAndEnabled)
        {


            highscoreTooltip.gameObject.SetActive(true);
            highscoreTooltip.SetTTVisibility(true, true);
            highscoreTooltip.SetTTArrowTarget(null);
            highscoreTooltip.Show("Glückwunsch, ihr habt alle Himmelskörper in diesem Level entdeckt!");

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
            lowEnergyTooltip.Hide();
        }

        if (SpaceshipGameplay.Instance.hitPoints / (float)SpaceshipGameplay.Instance.maxHitpoints < 0.2f)
        {
            Debug.Log("Show Health Tooltip");
            ShowLowHealthTooltip();
        }
        else
        {
            lowHealthTooltip.Hide();
        }


        if (Score.Instance.currentScore >= levelMaxScore)
        {

            
            ShowLevelHighScoreTooltip();
        }
    }
}

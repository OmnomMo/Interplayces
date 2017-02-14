using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

    public int energy;
    public int maxEnergy;

    public GameObject spaceShip;

    Image[] bars;

	// Use this for initialization
	void Start () {
        //Initialize();
	}

    public void Initialize()
    {
        
        bars = GetComponentsInChildren<Image>();
        maxEnergy = bars.Length;
        energy = 0;
        UpdateBars(energy);
    }

  
    public void UpdateBars(int nBars)
    {
        NetworkActions.Instance.CmdSetThrust(energy);

        int n = 0;

        foreach (Image i in bars)
        {
            if (n > nBars)
            {
                i.color = Color.grey;
            } else
            {
                i.color = Color.white;
            }

            n++;
        }

    }

    public void IncreaseEnergy()
    {
        if (energy < maxEnergy)
        {
            energy++;
        }

        UpdateBars(energy);
    }


    public void DecreaseEnergy()
    {
        if (energy > 0)
        {
            energy--;
        }

        UpdateBars(energy);
    }
    // Update is called once per frame
    void Update () {
	
	}
}

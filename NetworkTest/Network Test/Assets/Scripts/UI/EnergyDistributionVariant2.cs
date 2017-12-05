using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls and management for interface Variant 2, sets value of energy systems to value of onscreen sliders at every frame
//Manages display of Health and ENergy bar

public class EnergyDistributionVariant2 : MonoBehaviour {


    public GameObject batteryDisplay;
    public GameObject healthDisplay;
    public GameObject batteryPointer;
    public GameObject thrustSlider;
    public GameObject shieldSlider;
    public GameObject scanSlider;
    public GameObject shieldIconGrey;

    

    

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateSliders());
	}


    void OnDestroy()
    {
        Debug.Log("INterface2 Destroyed");
    }
    // Update is called once per frame
    void Update () {
        //hitpointsDisplay.text = hitPoints.ToString() + " HP";
        // hitpointsDisplay.GetComponent<RectTransform>().localScale = new Vector3((float)hitPoints/maxHitpoints, 1, 1);
  
        if (MainSceneManager.Instance == null)
        {
            Debug.Log("SceneManager Not Set!");
        }

           
            
            
       

        healthDisplay.GetComponent<Image>().fillAmount = (SpaceshipGameplay.Instance.hitPoints / (float)SpaceshipGameplay.Instance.maxHitpoints);

        //Debug.Log((float)SpaceshipGameplay.Instance.hitPoints / SpaceshipGameplay.Instance.maxHitpoints);

        


        if (SpaceshipGameplay.Instance.energyCapacity > 0)
        {
            //energyDisplay.GetComponent<RectTransform>().localScale = new Vector3(1, (float)energy / energyCapacity, 1);
            batteryDisplay.GetComponent<Image>().fillAmount = ((float)SpaceshipGameplay.Instance.energy / SpaceshipGameplay.Instance.energyCapacity) * 0.85f;
            batteryPointer.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90 - ((float)SpaceshipGameplay.Instance.energy / SpaceshipGameplay.Instance.energyCapacity * 150));
        }
    }

    public void UpdateThrust()
    {
        NetworkActions.Instance.CmdSetThrust(thrustSlider.GetComponent<Slider>().value);
    }
     public void UpdateShield()
    {
        NetworkActions.Instance.CmdSetShield(shieldSlider.GetComponent<Slider>().value);
    }

    public void UpdateScanner()
    {
        SpaceshipGameplay.Instance.scanPower = scanSlider.GetComponent<Slider>().value;
    }

    public IEnumerator UpdateSliders()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            UpdateThrust();
            UpdateShield();
            UpdateScanner();

        }
    }



    public void BalanceSliders(int changedSlider)
    {

        Slider[] sliders = new Slider[3];

        sliders[0] = thrustSlider.GetComponent<Slider>();
        sliders[1] = shieldSlider.GetComponent<Slider>();
        sliders[2] = scanSlider.GetComponent<Slider>();

       // Debug.Log((sliders[0].value + sliders[1].value + sliders[2].value));

        if ((sliders[0].value + sliders[1].value + sliders[2].value) > 1.0f)
        {
            //Debug.Log("StartBalancing");
            float delta = (sliders[0].value + sliders[1].value + sliders[2].value) - 1f;

            if (changedSlider != 0)
            {
                sliders[0].value -= (delta / 2f);
            }
            if (changedSlider != 1)
            {
                sliders[1].value -= (delta / 2f);
            }
            if (changedSlider != 2)
            {
                sliders[2].value -= (delta / 2f);
            }


            if (changedSlider == 0)
            {
                //If a slider falls beneath 0, value is substracted from other slider.
                if (sliders[1].value < 0)
                {
                    sliders[2].value += sliders[1].value;
                }
                if (sliders[2].value < 0)
                {
                    sliders[1].value += sliders[2].value;
                }
            }


            if (changedSlider == 1)
            {
                //If a slider falls beneath 0, value is substracted from other slider.
                if (sliders[0].value < 0)
                {
                    sliders[2].value += sliders[0].value;
                }
                if (sliders[2].value < 0)
                {
                    sliders[0].value += sliders[2].value;
                }
            }




            if (changedSlider == 2)
            {
                //If a slider falls beneath 0, value is substracted from other slider.
                if (sliders[1].value < 0)
                {
                    sliders[0].value += sliders[1].value;
                }
                if (sliders[0].value < 0)
                {
                    sliders[1].value += sliders[0].value;
                }
            }





            //clamp slider values

            if (sliders[0].value <= 0)
            {
                sliders[0].value = 0;
            }

            if (sliders[0].value >= 1)
            {
                sliders[0].value = 1;
            }

            if (sliders[1].value <= 0)
            {
                sliders[1].value = 0;
            }

            if (sliders[1].value >= 1)
            {
                sliders[1].value = 1;
            }


            if (sliders[2].value <= 0)
            {
                sliders[2].value = 0;
            }

            if (sliders[2].value >= 1)
            {
                sliders[2].value = 1;
            }



        }
    }
}

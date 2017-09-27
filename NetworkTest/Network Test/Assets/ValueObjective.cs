using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueObjective :  Objective{

    public enum possibleStats
    {
        thrust, shield, scan, health, energy, speed, score
    }

    public possibleStats checkedStat;
    public bool smallerThan;

    public float targetValue;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!completed)
        {
            switch (checkedStat)
            {
                case possibleStats.thrust:
                    if (SpaceshipGameplay.Instance.thrustPower > targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                    } else
                    {
                        if (smallerThan)
                        {
                            Complete();
                        }
                    }
                    break;
                case possibleStats.shield:
                    if (SpaceshipGameplay.Instance.shieldPower > targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                        else
                        {
                            if (smallerThan)
                            {
                                Complete();
                            }
                        }
                    }
                    break;
                case possibleStats.scan:
                    if (SpaceshipGameplay.Instance.scanPower > targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                        else
                        {
                            if (smallerThan)
                            {
                                Complete();
                            }
                        }
                    }
                    break;
                case possibleStats.health:
                    if (SpaceshipGameplay.Instance.hitPoints> targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                        else
                        {
                            if (smallerThan)
                            {
                                Complete();
                            }
                        }
                    }
                    break;
                case possibleStats.energy:
                    if (SpaceshipGameplay.Instance.energy > targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                    }
                    else
                    {
                        if (smallerThan)
                        {
                            Complete();
                        }
                    }
                    break;
                case possibleStats.speed:
                    if (Vector3.Magnitude(SpaceshipGameplay.Instance.gameObject.GetComponent<Rigidbody>().velocity) > targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                    }
                    else
                    {
                        if (smallerThan)
                        {
                            Complete();
                        }
                    }
                    break;
                case possibleStats.score:
                    if (SpaceshipGameplay.Instance.thrustPower > targetValue)
                    {
                        if (!smallerThan)
                        {
                            Complete();
                        }
                    }
                    else
                    {
                        if (smallerThan)
                        {
                            Complete();
                        }
                    }
                    break;
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlanetSOI : MonoBehaviour {

    public bool playerInSOI;
    public GameObject planet;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {


        for (int i = 0; i < LevelProgress.Instance.objectives.Length; i++)
        {
            //if one of the goals is to reach planet, set goal to completed;

            if (LevelProgress.Instance.objectives[i] is GoalObjective)
            {

                GoalObjective gO = (GoalObjective)LevelProgress.Instance.objectives[i];


                if (gO.goal == other)
                {
                    gO.Complete();
                }
            }

            if (LevelProgress.Instance.objectives[i] is MultiObjective)
            {

                MultiObjective mO = (MultiObjective)LevelProgress.Instance.objectives[i];
                for (int u = 0; u < mO.subObjectives.Length; u++)
                {
                    if (mO.subObjectives[u] is GoalObjective)
                    {

                        GoalObjective gO = (GoalObjective)mO.subObjectives[u];


                        if (gO.goal == other)
                        {
                            gO.Complete();
                        }
                    }

                }
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("PlanetSOI"))
        {

            if (planet != null)
            {
                planet.GetComponent<PlanetInfo>().HideInfo();
                planet.GetComponent<PlanetInfo>().playerStay = false;
            }

            playerInSOI = true;
            planet = other.gameObject.transform.parent.gameObject;
            planet.GetComponent<PlanetInfo>().ShowInfo();
            planet.GetComponent<PlanetInfo>().playerStay = true;
            Score.Instance.AddScanToPoints(planet);


           

            //Check for completed objectives
            //foreach (Objective o in LevelProgress.Instance.objectives)
            //{
            //    //if one of the goals is to reach planet, set goal to completed;

            //    if (o is GoalObjective)
            //    {

            //        if ((GoalObjective) o.goal == other)
            //        {
            //            o.Complete();
            //        }
            //    }

            //    if (o is MultiObjective)
            //    {
            //        foreach (Objective so in o.subObjectives)
            //        {

            //        }
            //    }

        

            //foreach (MultiObjective o in LevelProgress.Instance.objectives)
            //{
            //    foreach (GoalObjective gO in o.subObjectives)
            //    {
            //        if (gO.goal == other)
            //        {
            //            o.Complete();
            //        }
            //    }
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlanetSOI"))
        {
            playerInSOI = false;

            if (planet != null)
            {
                planet.GetComponent<PlanetInfo>().HideInfo();
                planet.GetComponent<PlanetInfo>().playerStay = false;
            }
        }
    }

    //In case player leaves SOI but is still in other SOI
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlanetSOI"))
        {
            if (!playerInSOI)
            {
                playerInSOI = true;
                planet = other.gameObject.transform.parent.gameObject;
                planet.GetComponent<PlanetInfo>().ShowInfo();
                planet.GetComponent<PlanetInfo>().playerStay = true;
                Score.Instance.AddScanToPoints(planet);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_RandomMovement : MonoBehaviour {

    public int NPC_ID;

    public bool movingRandomly;

    public bool isActive;

    public bool outOfLevelBounds;

    public GameObject levelBounds;

    public float maxDistanceCatch;
    public float maxDistanceReact;

    public bool isCaught;

    public float currentDistance;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SetDistanceToPlayer", 0f, 0.2f);
	}
	

    public float SetDistanceToPlayer()
    {
        if (SpaceshipGameplay.Instance != null)
        {
            return currentDistance = Vector3.Distance(transform.position, SpaceshipGameplay.Instance.gameObject.transform.position);
        } 
        else
        {
            return float.MaxValue;
        }
    }

	// Update is called once per frame
	void Update () {
		if (!isActive && movingRandomly)
        {
            isActive = true;

            if (GameState.Instance.isPlayerCaptain())
            {
                AccelerateRandomly();
            }
        }

        if (isActive && !movingRandomly)
        {
            isActive = false;
        }

        if (currentDistance < maxDistanceReact)
        {
            movingRandomly = true;
        }

        if (currentDistance < maxDistanceCatch && !isCaught)
        {
            if (GameState.Instance.isPlayerCaptain())
            {
                NetworkActions.Instance.CmdCatchNPC(NPC_ID);
            }
        }


        if (isCaught)
        {
            movingRandomly = false;
            CancelInvoke("SetDistanceToPlayer");
            Score.Instance.CatchAlien();
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LevelBounds"))
        {
            outOfLevelBounds = true;
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LevelBounds"))
        {
            outOfLevelBounds = false;
        }

    }

    void RandomMovement()
    {

        if (outOfLevelBounds)
        {

            transform.LookAt(levelBounds.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            AccelerateRandomly();


        }
        else
        {

            if (isActive)
            {

                float random = Random.Range(0f, 1f);

                if (random < 0.34f)
                {
                    TurnRandomly();
                    //Debug.Log("TurnRandomly");
                }
                else
                {
                    if (random < 0.67f)
                    {
                        AccelerateRandomly();
                        //Debug.Log("AccelerateRandomly");
                    }
                    else
                    {
                        PauseRandomly();
                        //Debug.Log("PauseRandomly");
                    }
                }
            }
        }
    }


    void TurnRandomly()
    {
        float direction = Random.Range(0f, 1f);
        float duration = Random.Range(0.05f, 0.3f);

        if (direction < 0.5f)
        {
            GetComponent<NPC_Movement>().StartTurningL();
            StartCoroutine(StopTurning(duration));
        } else
        {
            GetComponent<NPC_Movement>().StartTurningR();
            StartCoroutine(StopTurning(duration));
        }


    }

    IEnumerator StopTurning(float t)
    {
        yield return new WaitForSeconds(t);
        GetComponent<NPC_Movement>().StopTurningL();
        GetComponent<NPC_Movement>().StopTurningR();
        RandomMovement();
    }

    void PauseRandomly()
    {
        float duration = Random.Range(0.2f, 0.3f);
        StartCoroutine(StopPausing(duration));
    }

    IEnumerator StopPausing (float t)
    {
        yield return new WaitForSeconds(t);
        RandomMovement();
    }

    void AccelerateRandomly()
    {
        float duration = Random.Range(1f, 2f);
        GetComponent<NPC_Movement>().StartAccelerating();
        StartCoroutine(StopAccelerating(duration));
    }

    IEnumerator StopAccelerating(float t)
    {
        yield return new WaitForSeconds(t);
        GetComponent<NPC_Movement>().StopAccelerating();
        RandomMovement();
    }
}

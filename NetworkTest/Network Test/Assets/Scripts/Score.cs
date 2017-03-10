using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int currentScore;
    public Text scoreDisplay;
    public int scorePerScan;

    public List<GameObject> scannedObjects;
    public List<string> scannedObjectStrings;

    private static Score instance;
    public static Score Instance
    {
        get { return instance; }
    }

    public int AddScanToPoints(GameObject newScan)
    {
        //check if object is already scanned.
        if (scannedObjects.Contains(newScan))
        {
            return currentScore;
            Debug.Log("Object already scanned!");
        } else
        {
            currentScore += scorePerScan;
            scannedObjects.Add(newScan);


            string newScanName;

            if (newScan.GetComponent<PlanetInfo>() != null)
            {
                newScanName = newScan.GetComponent<PlanetInfo>().planetName;
            }
            else
            {
                if (newScan.GetComponentInChildren<PlanetInfo>() != null)
                {
                    newScanName = newScan.GetComponentInChildren<PlanetInfo>().planetName;
                }
                else
                {
                    newScanName = "placeholderPlanetName";
                }
            }

            scannedObjectStrings.Add(newScanName);

            return currentScore;
        }

        //if not

        
    }

    // Use this for initialization
    void Start () {
        instance = this;
        scannedObjects = new List<GameObject>();
        scannedObjectStrings = new List<string>();
	}



    public int GetScore()
    {
        return currentScore;
    }

    public int AddScore(int scorePlus)
    {
        currentScore += scorePlus;
        return currentScore;
    }
	
	// Update is called once per frame
	void Update () {

        if (!ToEndScreen.Instance.hasEnded)
        {
         //   scoreDisplay.text = currentScore.ToString();
        }
	}
}

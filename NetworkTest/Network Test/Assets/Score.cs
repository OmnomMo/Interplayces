using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int currentScore;
    public Text scoreDisplay;
    public int scorePerScan;

    public List<GameObject> scannedObjects;

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
            return currentScore;
        }

        //if not

        
    }

    // Use this for initialization
    void Start () {
        instance = this;
        scannedObjects = new List<GameObject>();
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
        scoreDisplay.text =  currentScore.ToString();
	}
}

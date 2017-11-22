using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePartPositions : MonoBehaviour {

    public Vector3[] allPositions;
    public GameObject[] allPuzzleParts;



    private static PuzzlePartPositions instance;
    public static PuzzlePartPositions Instance
    {
        get { return instance; }
    }
    void Awake()
    {

        if (PuzzlePartPositions.Instance == null)
        {


            instance = this;

        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }

        
    }


    // Use this for initialization
    void Start () {
		
	}


    //Collects all gameobjects with component "FollowSphere" (all puzzleparts) in game Scene and stores them in array
    public void CollectParts()
    {
        FollowSphere[] tempParts = FindObjectsOfType(typeof(FollowSphere)) as FollowSphere[];

        allPuzzleParts = new GameObject[tempParts.Length];

        for (int i = 0; i < tempParts.Length; i++)
        {
            foreach (FollowSphere fs in tempParts)
            {
                if (fs.UID == i)
                {
                    allPuzzleParts[i] = fs.gameObject;
                }
            }

            if (allPuzzleParts[i] == null)
            {
                Debug.Log("ERROR: Array too long. Shortening array");

               GameObject[] finalAllPuzzleParts = new GameObject[i];

                for (int u = 0; u < i; u++)
                {
                    finalAllPuzzleParts[u] = allPuzzleParts[u];
                }

                allPuzzleParts = finalAllPuzzleParts;

                break;
            }
        }
    }

    //Stores position of all PuzzleParts in Array
    public void SavePartPositions()
    {

        if (allPuzzleParts != null)
        {

            allPositions = new Vector3[allPuzzleParts.Length];

            for (int i = 0; i < allPuzzleParts.Length; i++)
            {
                allPositions[i] = allPuzzleParts[i].GetComponent<FollowSphere>().sphere.transform.position;
            }
        }
    }

    //Restores positions of all puzzleParts from Array
    public void RestorePartPositions()
    {
        if (allPuzzleParts.Length > 0 && allPositions.Length >0) {
            for (int i = 0; i < allPuzzleParts.Length; i++)
            {
                allPuzzleParts[i].GetComponent<FollowSphere>().sphere.transform.position = allPositions[i];
            }
        }
    }
	

    //Checks if parts are on playing field and includes them. Run this after restoring part positions
    public void IncludeParts()
    {
        if (allPuzzleParts != null)
        {
            foreach (GameObject part in allPuzzleParts)
            {
                if (part.GetComponent<FollowSphere>().isOnField)
                {
                    Debug.Log("include part " + part.GetComponent<FollowSphere>().UID);
                    PlayingGrid.Instance.IncludePieceV02(part);
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

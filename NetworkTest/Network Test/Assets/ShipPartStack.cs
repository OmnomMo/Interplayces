using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPartStack : MonoBehaviour {

    public int maxParts;

    public int currentParts;

    public GameObject shipPuzzlePart;
    public GameObject dragHandle;

    public GameObject playingField;

    public Text infoDisplay;


    float spawnZ = 0;


    public bool SpawnPuzzlePart()
    {
        if (currentParts == 0)
        {
            return false;
        }

        currentParts--;


        GameObject newPuzzlePart = GameObject.Instantiate(shipPuzzlePart);
        newPuzzlePart.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, spawnZ);

        GameObject newDragHandle = GameObject.Instantiate(dragHandle);
        newDragHandle.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, spawnZ-5);

        newDragHandle.GetComponent<DragAround>().partContainer = newPuzzlePart;
        newDragHandle.GetComponent<DragAround>().partStack = this.gameObject;
        newPuzzlePart.GetComponent<FollowSphere>().sphere = newDragHandle;
        newPuzzlePart.GetComponent<FollowSphere>().playingField = playingField;


        //Set Puzzle part to be tracked
        //dragHandle.GetComponent<DragAround>().dragged = true;
        //NetworkActions.Instance.CmdDragSphere(newDragHandle);
        StartCoroutine(DragPuzzlePart(newDragHandle.GetComponent<DragAround>()));

        UpdateInfoDisplay();

        return true;

    }

    public IEnumerator DragPuzzlePart(DragAround dragHandle)
    {
        yield return null;
        dragHandle.dragged = true;
    }

    void OnMouseDown()
    {
        if (GameState.Instance.isPlayerCaptain())
        {

            if (!SpawnPuzzlePart())
            {
                Debug.Log("No puzzle parts left in this stack");
            }
        }

    }


    // Use this for initialization
    void Start () {
        currentParts = maxParts;

        UpdateInfoDisplay();

        
	}


    public void ReturnPart()
    {
        if (currentParts < maxParts) {
            currentParts++;
                }
        UpdateInfoDisplay();
    }

    public void UpdateInfoDisplay()
    {
        infoDisplay.text = currentParts + "/" + maxParts; 
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

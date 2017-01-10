using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBuilding : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void EndPhase()
    {

        SpaceShipPlans.Instance.ClearAllParts();


        //Get List of Ship parts on grid;
        LinkedList<GameObject> shipParts = PlayingGrid.Instance.GetAllParts();

        var currentNode = shipParts.First;

        //Iterate over list and add to plan;
        while ((currentNode!=null))
        {
            GameObject part = currentNode.Value;
            ShipPart sPart = null;

            //Gets all Components of type shippart
            Component[] partComponents = part.GetComponentsInChildren(typeof(ShipPart));

            for (int i = 0; i < partComponents.Length; i++)
            {
                if (partComponents[i] is ShipPart)
                {
                    sPart = partComponents[i] as ShipPart;
                }
            }

            Debug.Log(sPart);

            SpaceShipPlans.Instance.AddPart(sPart.getID(), sPart.GetPosX(), sPart.GetPosY());
            currentNode = currentNode.Next;
        }

        Debug.Log(SpaceShipPlans.Instance.ToString());

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

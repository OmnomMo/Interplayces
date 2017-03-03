using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneDisplay : MonoBehaviour {

    public Text reasonText;
    public Text scoreText;
    public Text foundObjectsText;
    

	// Use this for initialization
	void Start () {

        if (GameState.Instance == null)
        {
            Application.LoadLevel(0);
        }

        StartCoroutine(GetScore());
	}

    IEnumerator GetScore()
    {
        yield return null;

        scoreText.text = Score.Instance.GetScore().ToString();

        ToEndScreen.reasonForTermination reason = ToEndScreen.Instance.GetReason();

        if (reason == ToEndScreen.reasonForTermination.hp)
        {
            reasonText.text = "Ihr seid zu schnell in einen Asteroiden gekracht!";
        }
        else
        {
            if (reason == ToEndScreen.reasonForTermination.energy)
            {
                reasonText.text = "Euch ist die Energie ausgegangen!";
            }
            else
            {
                if (reason == ToEndScreen.reasonForTermination.player)
                {
                    reasonText.text = "Die Expedition wurde abgebrochen.";
                }
                else
                {
                    reasonText.text = "Ende.";
                }
            }
        }

        string planetList = "";

        for (int i = 0; i < Score.Instance.scannedObjectStrings.Count; i++)
        {
           planetList += ( "-" + Score.Instance.scannedObjectStrings[i] + "\n");
        }

        foundObjectsText.text = planetList;

        GameObject.Destroy(ToEndScreen.Instance.gameObject);

    }

    public void NewShip()
    {
        MultiplayerSetup.Instance.ServerChangeScene("SpaceShipEditor");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
